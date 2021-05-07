#include <windows.h>
#include <time.h>

void _stdcall Unswizzle(void *src, unsigned int depth, unsigned int width, unsigned int height, void *dest)
{
  for (UINT y = 0; y < height; y++)
  {
    UINT sy = 0;
    if (y < width)
    {
      for (int bit = 0; bit < 16; bit++)
        sy |= ((y >> bit) & 1) << (2*bit);
      sy <<= 1; // y counts twice
    }
    else
    {
      UINT y_mask = y % width;
      for (int bit = 0; bit < 16; bit++)
        sy |= ((y_mask >> bit) & 1) << (2*bit);
      sy <<= 1; // y counts twice
      sy += (y / width) * width * width;
    }
    BYTE *d = (BYTE *)dest + y * width * depth;
    for (UINT x = 0; x < width; x++)
    {
      UINT sx = 0;
      if (x < height * 2)
      {
        for (int bit = 0; bit < 16; bit++)
          sx |= ((x >> bit) & 1) << (2*bit);
      }
      else
      {
        int x_mask = x % (2*height);
        for (int bit = 0; bit < 16; bit++)
          sx |= ((x_mask >> bit) & 1) << (2*bit);
        sx += (x / (2 * height)) * 2 * height * height;
      }
      BYTE *pSource = (BYTE *)src + (sx + sy)*depth;
      for (unsigned int i = 0; i < depth; ++i)
        *d++ = *pSource++;
    }
  }
}

void DXT1toARGB(void *src, void *dest, unsigned int destWidth)
{
  BYTE *b = (BYTE *)src;
  // colour is in R5G6B5 format, convert to R8G8B8
  DWORD colour[4];
  BYTE red[4];
  BYTE green[4];
  BYTE blue[4];
  for (int i = 0; i < 2; i++)
  {
    red[i] = b[2*i+1] & 0xf8;
    green[i] = ((b[2*i+1] & 0x7) << 5) | ((b[2*i] & 0xe0) >> 3);
    blue[i] = (b[2*i] & 0x1f) << 3;
    colour[i] = (red[i] << 16) | (green[i] << 8) | blue[i];
  }
  if (colour[0] > colour[1])
  {
    red[2] = (2 * red[0] + red[1] + 1) / 3;
    green[2] = (2 * green[0] + green[1] + 1) / 3;
    blue[2] = (2 * blue[0] + blue[1] + 1) / 3;
    red[3] = (red[0] + 2 * red[1] + 1) / 3;
    green[3] = (green[0] + 2 * green[1] + 1) / 3;
    blue[3] = (blue[0] + 2 * blue[1] + 1) / 3;
    for (int i = 0; i < 4; i++)
      colour[i] = (red[i] << 16) | (green[i] << 8) | blue[i] | 0xFF000000;
  }
  else
  {
    red[2] = (red[0] + red[1]) / 2;
    green[2] = (green[0] + green[1]) / 2;
    blue[2] = (blue[0] + blue[1]) / 2;
    for (int i = 0; i < 3; i++)
      colour[i] = (red[i] << 16) | (green[i] << 8) | blue[i] | 0xFF000000;
    colour[2] = 0;  // transparent
  }
  // ok, now grab the bits
  for (int y = 0; y < 4; y++)
  {
    DWORD *d = (DWORD *)dest + destWidth * y;
    *d++ = colour[(b[4 + y] & 0x03)];
    *d++ = colour[(b[4 + y] & 0x0e) >> 2];
    *d++ = colour[(b[4 + y] & 0x30) >> 4];
    *d++ = colour[(b[4 + y] & 0xe0) >> 6];
  }
}

void DXT3toARGB(void *src, void *dest, unsigned int destWidth)
{
  BYTE *b = (BYTE *)src;
  // colour is in R5G6B5 format, convert to R8G8B8
  DWORD colour[4];
  BYTE red[4];
  BYTE green[4];
  BYTE blue[4];
  BYTE alpha[4][4];

  alpha[0][0] = ((b[0] & 0x0f) << 4) | (b[0] & 0x0f);
  alpha[0][1] = (b[0] & 0xf0) | ((b[0] & 0xf0) >> 4);
  alpha[0][2] = ((b[1] & 0x0f) << 4) | (b[1] & 0x0f);
  alpha[0][3] = (b[1] & 0xf0) | ((b[1] & 0xf0) >> 4);

  alpha[1][0] = ((b[2] & 0x0f) << 4) | (b[2] & 0x0f);
  alpha[1][1] = (b[2] & 0xf0) | ((b[2] & 0xf0) >> 4);
  alpha[1][2] = ((b[3] & 0x0f) << 4) | (b[3] & 0x0f);
  alpha[1][3] = (b[3] & 0xf0) | ((b[3] & 0xf0) >> 4);

  alpha[2][0] = ((b[4] & 0x0f) << 4) | (b[4] & 0x0f);
  alpha[2][1] = (b[4] & 0xf0) | ((b[4] & 0xf0) >> 4);
  alpha[2][2] = ((b[5] & 0x0f) << 4) | (b[5] & 0x0f);
  alpha[2][3] = (b[5] & 0xf0) | ((b[5] & 0xf0) >> 4);

  alpha[3][0] = ((b[6] & 0x0f) << 4) | (b[6] & 0x0f);
  alpha[3][1] = (b[6] & 0xf0) | ((b[6] & 0xf0) >> 4);
  alpha[3][2] = ((b[7] & 0x0f) << 4) | (b[7] & 0x0f);
  alpha[3][3] = (b[7] & 0xf0) | ((b[7] & 0xf0) >> 4);
  
  b = (BYTE *)src + 8;

  for (int i = 0; i < 2; i++)
  {
    red[i] = b[2*i+1] & 0xf8;
    green[i] = ((b[2*i+1] & 0x7) << 5) | ((b[2*i] & 0xe0) >> 3);
    blue[i] = (b[2*i] & 0x1f) << 3;
    colour[i] = (red[i] << 16) | (green[i] << 8) | blue[i];
  }

  red[2] = (2 * red[0] + red[1] + 1) / 3;
  green[2] = (2 * green[0] + green[1] + 1) / 3;
  blue[2] = (2 * blue[0] + blue[1] + 1) / 3;
  red[3] = (red[0] + 2 * red[1] + 1) / 3;
  green[3] = (green[0] + 2 * green[1] + 1) / 3;
  blue[3] = (blue[0] + 2 * blue[1] + 1) / 3;
  for (int i = 0; i < 4; i++)
	colour[i] = (red[i] << 16) | (green[i] << 8) | blue[i];
  
  // ok, now grab the bits
  for (int y = 0; y < 4; y++)  
  {   
	DWORD *d = (DWORD *)dest + destWidth * y;
	*d++ = colour[(b[4 + y] & 0x03)] | (alpha[y][0] << 24);
	*d++ = colour[(b[4 + y] & 0x0e) >> 2] | (alpha[y][1] << 24);
	*d++ = colour[(b[4 + y] & 0x30) >> 4] | (alpha[y][2] << 24);
	*d++ = colour[(b[4 + y] & 0xe0) >> 6] | (alpha[y][3] << 24);
  }
}

void DXT5toARGB(void *src, void *dest, unsigned int destWidth)
{
  BYTE *b = (BYTE *)src;
  BYTE alpha[8];
  alpha[0] = b[0];
  alpha[1] = b[1];
  if (alpha[0] > alpha[1])
  {
    alpha[2] = (6 * alpha[0] + 1 * alpha[1]+ 3) / 7;
    alpha[3] = (5 * alpha[0] + 2 * alpha[1] + 3) / 7;    // bit code 011
    alpha[4] = (4 * alpha[0] + 3 * alpha[1] + 3) / 7;    // bit code 100
    alpha[5] = (3 * alpha[0] + 4 * alpha[1] + 3) / 7;    // bit code 101
    alpha[6] = (2 * alpha[0] + 5 * alpha[1] + 3) / 7;    // bit code 110
    alpha[7] = (1 * alpha[0] + 6 * alpha[1] + 3) / 7;    // bit code 111  
  }
  else
  {  
    alpha[2] = (4 * alpha[0] + 1 * alpha[1] + 2) / 5;    // Bit code 010
    alpha[3] = (3 * alpha[0] + 2 * alpha[1] + 2) / 5;    // Bit code 011
    alpha[4] = (2 * alpha[0] + 3 * alpha[1] + 2) / 5;    // Bit code 100
    alpha[5] = (1 * alpha[0] + 4 * alpha[1] + 2) / 5;    // Bit code 101
    alpha[6] = 0;                                      // Bit code 110
    alpha[7] = 255;                                    // Bit code 111  
  }
  // ok, now grab the bits
  BYTE a[4][4];
  a[0][0] = alpha[(b[2] & 0xe0) >> 5];
  a[0][1] = alpha[(b[2] & 0x1c) >> 2];
  a[0][2] = alpha[((b[2] & 0x03) << 1) | ((b[3] & 0x80) >> 7)];
  a[0][3] = alpha[(b[3] & 0x70) >> 4];
  a[1][0] = alpha[(b[3] & 0x0e) >> 1];
  a[1][1] = alpha[((b[3] & 0x01) << 2) | ((b[4] & 0xc0) >> 6)];
  a[1][2] = alpha[(b[4] & 0x38) >> 3];
  a[1][3] = alpha[(b[4] & 0x07)];
  a[2][0] = alpha[(b[5] & 0xe0) >> 5];
  a[2][1] = alpha[(b[5] & 0x1c) >> 2];
  a[2][2] = alpha[((b[5] & 0x03) << 1) | ((b[6] & 0x80) >> 7)];
  a[2][3] = alpha[(b[6] & 0x70) >> 4];
  a[3][0] = alpha[(b[6] & 0x0e) >> 1];
  a[3][1] = alpha[((b[6] & 0x01) << 2) | ((b[7] & 0xc0) >> 6)];
  a[3][2] = alpha[(b[7] & 0x38) >> 3];
  a[3][3] = alpha[(b[7] & 0x07)];

  b = (BYTE *)src + 8;
  // colour is in R5G6B5 format, convert to R8G8B8
  DWORD colour[4];
  BYTE red[4];
  BYTE green[4];
  BYTE blue[4];
  for (int i = 0; i < 2; i++)
  {
    red[i] = b[2*i+1] & 0xf8;
    green[i] = ((b[2*i+1] & 0x7) << 5) | ((b[2*i] & 0xe0) >> 3);
    blue[i] = (b[2*i] & 0x1f) << 3;
  }
  red[2] = (2 * red[0] + red[1] + 1) / 3;
  green[2] = (2 * green[0] + green[1] + 1) / 3;
  blue[2] = (2 * blue[0] + blue[1] + 1) / 3;
  red[3] = (red[0] + 2 * red[1] + 1) / 3;
  green[3] = (green[0] + 2 * green[1] + 1) / 3;
  blue[3] = (blue[0] + 2 * blue[1] + 1) / 3;


  for (int i = 0; i < 4; i++)
    colour[i] = (red[i] << 16) | (green[i] << 8) | blue[i];
  // and assign them to our texture
  for (int y = 0; y < 4; y++)
  {
    DWORD *d = (DWORD *)dest + destWidth * y;
    *d++ = colour[(b[4 + y] & 0x03)] | (a[y][0] << 24);
    *d++ = colour[(b[4 + y] & 0x0e) >> 2] | (a[y][1] << 24);
    *d++ = colour[(b[4 + y] & 0x30) >> 4] | (a[y][2] << 24);
    *d++ = colour[(b[4 + y] & 0xe0) >> 6] | (a[y][3] << 24);
  }

}

void _stdcall ConvertDXT1(void *src, unsigned int width, unsigned int height, void *dest)
{
  for (unsigned int y = 0; y < height; y += 4)
  {
    for (unsigned int x = 0; x < width; x += 4)
    {
      BYTE *s = (BYTE *)src + y * width / 2 + x * 2;
      DWORD *d = (DWORD *)dest + y * width + x;
      DXT1toARGB(s, d, width);
    }
  }
}

void _stdcall ConvertDXT3(void *src, unsigned int width, unsigned int height, void *dest)
{
  for (unsigned int y = 0; y < height; y += 4)
  {
    for (unsigned int x = 0; x < width; x += 4)
    {
      BYTE *s = (BYTE *)src + y * width + x * 4;
      DWORD *d = (DWORD *)dest + y * width + x;
      DXT3toARGB(s, d, width);
    }
  }
}

void _stdcall ConvertDXT5(void *src, unsigned int width, unsigned int height, void *dest)
{
  for (unsigned int y = 0; y < height; y += 4)
  {
    for (unsigned int x = 0; x < width; x += 4)
    {
      BYTE *s = (BYTE *)src + y * width + x * 4;
      DWORD *d = (DWORD *)dest + y * width + x;
      DXT5toARGB(s, d, width);
    }
  }
}

void _stdcall ConvertA1R5G5B5(void *src, unsigned int width, unsigned int height, void *dest)
{
  for (unsigned int y = 0; y < height; y += 1)
  {
    for (unsigned int x = 0; x < width; x += 1)
    {
      BYTE *s = (BYTE *)src + (y * (width * 2)) + (x * 2);
      DWORD *d = (DWORD *)dest + y * width + x;

      DWORD colour;
      BYTE alpha;
      BYTE red;
      BYTE green;
      BYTE blue;

      red = (s[1] & 0x7c)<<1; 
      green = ((s[0] & 0xe0) >> 2) | ((s[1] & 0x03) << 6);
      blue = (s[0] & 0x1f) << 3;
      alpha = ((s[1] & 0x80) >> 7) * 255; 
      colour = (alpha << 24) | (red << 16) | (green << 8) | blue;
      *d = colour;
    }
  }
}

void _stdcall MapPalette(void *pal, void *src, unsigned int width, unsigned int height, void *dest)
{
  BYTE *p = (BYTE *)pal;
  BYTE *b = (BYTE *)src;
  BYTE *d = (BYTE *)dest;
  BYTE color;
  for (unsigned int y = 0; y < height; y += 1)
  {
    for (unsigned int x = 0; x < width; x += 1)
    {
	  color=b[(y*width)+x];
	  d[(y*width*4)+(x*4)]=p[color*4];
	  d[(y*width*4)+(x*4)+1]=p[(color*4)+1];
	  d[(y*width*4)+(x*4)+2]=p[(color*4)+2];
	  d[(y*width*4)+(x*4)+3]=p[(color*4)+3];
    }
  }
}
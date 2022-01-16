#ifndef _Direction_
#define _Direction_


#define FLAG_0         (1 << 0) // 1
#define FLAG_1         (1 << 1) // 2
#define FLAG_2         (1 << 2) // 4
#define FLAG_3         (1 << 3) // 8
#define FLAG_4         (1 << 4) // 16
#define FLAG_5         (1 << 5) // 32
#define FLAG_6         (1 << 6) // 64
#define FLAG_7         (1 << 7) // 128

#define SET_FLAG(n, f) ((n) |= (f)) 
#define CLR_FLAG(n, f) ((n) &= ~(f)) 
#define TGL_FLAG(n, f) ((n) ^= (f)) 
#define CHK_FLAG(n, f) ((n) & (f))


#endif
using System;

namespace Random_Baby
{
    public class MyRandom
    {
        int a, b, c, x0, x;
        public MyRandom(int x0, int a, int b, int c)
        {
            this.x0 = x0;
            this.a = a;
            this.b = b;
            this.c = c;
            x = x0;
        }

        public Int32 next()
        {
            x = (a * x + b) % c;
            return x;
        }
    }
}

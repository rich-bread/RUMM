using System;

namespace RUMM.Method
{
    public static class Trimmode
    {
        public static string numbered_x, numbered_z;

        public static void Coordinate(string center_x, string center_z, string x, string z)
        {
            //指定されたX・Z座標から、中心座標を引く
            int ax = int.Parse(x) - int.Parse(center_x);
            int az = (int.Parse(z) * -1) - int.Parse(center_z);     //Minecraftの座標仕様に合わせる為、Z座標を反転

            //X・Zの最大値が、範囲外にならないようにする
            int rax, raz;

            if (Math.Sign(ax) > 0)
            {
                rax = (ax + 64) - 1;
            }
            else if (Math.Sign(ax) < 0)
            {
                rax = (ax - 64) + 1;
            }
            else
            {
                rax = ax + 64;
            }

            if (Math.Sign(az) > 0)
            {
                raz = (az + 64) - 1;
            }
            else if (Math.Sign(az) < 0)
            {
                raz = (az - 64) + 1;
            }
            else
            {
                raz = az + 64;
            }

            //
            double fax, faz, fx, fz;

            fax = rax / 128;
            faz = raz / 128;

            fx = Math.Truncate(fax);
            if (fx == -0)
            {
                fx = 0;
            }

            fz = Math.Truncate(faz) * -1;
            if (fz == -0)
            {
                fz = 0;
            }

            numbered_x = fx.ToString();
            numbered_z = fz.ToString();
        }
    }
}

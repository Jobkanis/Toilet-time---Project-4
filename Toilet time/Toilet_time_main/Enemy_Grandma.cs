﻿using System;

namespace Toilet_time_main
{
    public class Enemy_Grandma : Fallable_Object
    {
        int xstart;
        int region;
        int steps_out;
        bool returning;
        float new_x_add = 0;

        int speed;
        int x_addition;

        Position startposition;

        public Enemy_Grandma(int x_pos, int y_pos, int region, int speed)
            : base(new Position(x_pos, y_pos), new Size(20, 40), true)
        {
            this.region = region;
            this.speed = speed;
            xstart = x_pos;
            returning = false;
            this.startposition = new Position(x_pos, y_pos);
        }

        

        public override void Update(float dt, Gui_Manager guimanager)
        {
            base.Update(dt, guimanager);

            if (returning)
            {
                if (steps_out > 0)
                {
                    new_x_add -= speed * dt;
                }
                else
                {
                    returning = false;
                }
            }
            else
            {
                if (steps_out < region)
                {
                    new_x_add += speed * dt;
                }
                else
                {
                    returning = true;
                }
            }

            x_addition = (int)(new_x_add);
            this.steps_out += x_addition;

            new_x_add = new_x_add - x_addition;
            Fallable_Object main = guimanager.GetMain_Character();

            if (!((this.position.x + x_addition + 2 < main.position.x + main.size.x && this.position.x + x_addition - 2 + this.size.x > main.position.x && this.position.y + this.size.y > main.position.y && this.position.y < main.position.y + main.size.y)))
            {

                this.position.x += x_addition;
            }
            else
            {
                guimanager.Main_Dead();
            }

            if (this.position.y > 600)
            {
                Console.WriteLine("respawning");
                this.position.y = startposition.y;
            }

        }

        public override void Draw(iDrawVisitor visitor)
        {
            visitor.DrawEnemyGrandma(this);
        }
    }
}
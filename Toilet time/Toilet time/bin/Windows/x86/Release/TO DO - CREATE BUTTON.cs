﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Toilet_time
{
    public class Button : iObject
    {
        public Label label;
        Color regularcolor;
        Color mouseovercolor;
        public Action<Gui_Manager> action;

        int cooldown = 0;

        public Button(int x_pos, int y_pos, int x_size, int y_size, string text, Color regularcolor, Color mouseovercolor, Action<Gui_Manager> action)
            :base(new Position(x_pos, y_pos), new Size(x_size,y_size), true)
        {
            this.label = new Label(x_pos, y_pos, x_size, y_size, text);

            this.action = action;
            this.regularcolor = regularcolor;
            this.mouseovercolor = mouseovercolor;
        }
        public override void Draw(DrawVisitor visitor)
        {
            visitor.DrawButton(this);
            visitor.DrawLabel(this.label);
        }

        private bool isIntersecting(Point point)
        {
            return point.x > this.position.x && point.y > this.position.y&&
           point.x < this.position.x + this.size.x && point.y < this.position.y + this.size.y;

        }

        public override void Update(float dt, Gui_Manager guimanager)
        {
            if (cooldown > 0) { cooldown = cooldown - 1; }

            if (guimanager.LatestInput != null)
            {
                if (isIntersecting(guimanager.Cursor))
                {
                    this.color = this.mouseovercolor;

                    if (guimanager.LatestInput.MouseButton.Visit<bool>(() => false, _ => true) && cooldown <= 0)
                    {
                        if (guimanager.LatestInput.MouseButton.Visit<MousePressed>(() => { throw new Exception("mousepressed failed"); }, item => { return item; }) == MousePressed.Left_Button)
                        {
                            action(guimanager);
                            cooldown = 10;
                        }
                    }
                }
                else
                {
                    this.color = this.regularcolor;
                }
            }
        }
    }
}
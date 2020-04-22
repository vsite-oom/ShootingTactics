using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShootingTactics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            int size = CreateButtons();
            AddLabels(size);
        }

        private void AddLabels(int size)
        {
            int y = panelMain.Top;
            for (int r = 0; r < rows; ++r)
            {
                Label label = new Label {
                    Top = y,
                    Left = panelMain.Left - size,
                    Width = size,
                    Height = size,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = (r + 1).ToString()
                };
                Controls.Add(label);
                y += size;
            }
            int x = panelMain.Left;
            for (int c = 0; c < columns; ++c)
            {
                Label label = new Label
                {
                    Top = panelMain.Top - size,
                    Left = x,
                    Width = size,
                    Height = size,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = ((char)(c + 'A')).ToString()
                };
                Controls.Add(label);
                x += size;
            }
        }

        private int CreateButtons()
        {
            buttons = new CheckBox[rows, columns];
            int buttonSize = Math.Min(panelMain.Width / columns, panelMain.Height / rows);

            int x0 = (panelMain.Width - columns * buttonSize) / 2;
            int y = (panelMain.Height - rows * buttonSize) / 2;
            for (int r = 0; r < rows; ++r)
            {
                int x = x0;
                for (int c = 0; c < columns; ++c)
                {
                    CheckBox button = new CheckBox
                    {
                        Top = y,
                        Left = x,
                        Width = buttonSize,
                        Height = buttonSize,
                        Appearance = Appearance.Button
                    };
                    button.CheckStateChanged += Button_CheckStateChanged;
                    buttons[r, c] = button;
                    panelMain.Controls.Add(button);
                    x += buttonSize;
                }
                y += buttonSize;
            }
            return buttonSize;
        }

        Square FindHitSquare(CheckBox button)
        {
            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < columns; ++c)
                {
                    if (button == buttons[r, c])
                        return new Square(r, c);
                }
            }
            return null;
        }

        private void Button_CheckStateChanged(object sender, EventArgs e)
        {
            CheckBox button = (CheckBox)sender;
            if (button.Checked)
            {
                Square square = FindHitSquare(button);
                var hit = fleet.IsHit(square);
                switch (hit)
                {
                    case HitResult.Hit:
                        button.BackColor = Color.Red;
                        break;
                    case HitResult.Sunk:
                        button.BackColor = Color.Crimson;
                        break;
                    default:
                        button.BackColor = SystemColors.Window;
                        break;
                }
            }
        }

        CheckBox[,] buttons;
        int rows = 10;
        int columns = 10;
        Color buttonColor = SystemColors.ControlLight;
        Color shipColor = Color.Brown;
        Fleet fleet = new Fleet();

        private void ResetButtons()
        {
            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < columns; ++c)
                {
                    buttons[r, c].Checked = false;
                    buttons[r, c].BackColor = buttonColor;
                }
            }
            List<Ship> ships = new List<Ship>(fleet.Ships);
            ships.Sort((s1, s2) => s2.Squares.Count() - s1.Squares.Count());
            for (int i = 1; i < ships.Count(); i += 2)
            {
                foreach (var square in ships[i].Squares)
                {
                    var button = buttons[square.Row, square.Column];
                    button.Checked = true;
                    button.BackColor = shipColor;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Shipwright sw = new Shipwright(rows, columns);
            fleet = sw.CreateFleet(new int[]{ 5, 4, 4, 3, 3, 3, 2, 2, 2, 2});
            ResetButtons();
        }
    }
}

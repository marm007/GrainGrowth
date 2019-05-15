using System;
using System.Drawing;
using System.Windows.Forms;
using static Config;

public class Grid
{

    private System.Drawing.Pen circuitPen = new System.Drawing.Pen(Color.Black, 1);
    private System.Drawing.Pen circuitPenClear = new System.Drawing.Pen(SystemColors.Control, 1);
    private System.Drawing.SolidBrush cellBrushClear = new System.Drawing.SolidBrush(SystemColors.Control);

    private int sizeX;
    private int sizeY;

    public int SizeX { get => sizeX; set => sizeX = value; }
    public int SizeY { get => sizeY; set => sizeY = value; }

    public Grid(int sizeX, int sizeY)
	{
        this.sizeX = sizeX;
        this.sizeY = sizeY;
	}

    public Grid(PictureBox pictureBox)
	{
        this.sizeX = pictureBox.Width / CELL_SIZE;
        this.sizeY = pictureBox.Height / CELL_SIZE;
        Console.WriteLine(this.sizeX);

    }

    public void Draw(PictureBox pictureBox, System.Drawing.Graphics gg, Simulation simulation,
        int sizeX, int sizeY, bool drawX, bool drawY)
    {
        if (GRID_STATE == GridState.Disable)
            return;

        Console.WriteLine("DRAWING...");
        System.Drawing.Graphics g = pictureBox.CreateGraphics();

        int maxSizeX = pictureBox.Width / CELL_SIZE;
        int maxSizeY = pictureBox.Height / CELL_SIZE;

        if (!drawX)
            maxSizeX = sizeX;

        if (!drawY)
            maxSizeY = sizeY;

        Console.WriteLine(maxSizeX);
        Console.WriteLine(sizeX);

        if (maxSizeX == sizeX)
        {
            // zmienia sie Y

            if (maxSizeY > sizeY && drawY)
            {

                for (int i = 0; i < sizeX; i++)
                {
                    for (int j = sizeY; j < maxSizeY; j++)
                    {
                        g.DrawRectangle(circuitPen, i * CELL_SIZE, j * CELL_SIZE,
                            CELL_SIZE, CELL_SIZE);
                    }
                }
            }
            else if (maxSizeY < sizeY && drawY)
            {
                for (int i = 0; i < sizeX; i++)
                {
                    for (int j = maxSizeY; j < sizeY; j++)
                    {
                        simulation.Display(g, i, j, 0);

                        g.DrawRectangle(circuitPenClear, i * CELL_SIZE, j * CELL_SIZE + 1,
                            CELL_SIZE, CELL_SIZE);
                    }
                }
            }



        }
        else if (maxSizeX < sizeX)
        {

            if (maxSizeY > sizeY)
            {
                if (drawX)
                {
                    for (int i = maxSizeX; i < sizeX; i++)
                    {
                        for (int j = 0; j < maxSizeY; j++)
                        {

                            if (sizeY < maxSizeY)
                                simulation.Display(g, i, j, 0);

                            g.DrawRectangle(circuitPenClear, i * CELL_SIZE + 1, j * CELL_SIZE,
                                CELL_SIZE, CELL_SIZE);
                        }
                    }
                }

                if (drawY)
                {
                    for (int i = 0; i < maxSizeX; i++)
                    {
                        for (int j = sizeY; j < maxSizeY; j++)
                        {

                            g.DrawRectangle(circuitPen, i * CELL_SIZE, j * CELL_SIZE,
                                CELL_SIZE, CELL_SIZE);
                        }
                    }
                }

            }
            else if (maxSizeY < sizeY)
            {
                if (drawX)
                {
                    for (int i = maxSizeX; i < sizeX; i++)
                    {
                        for (int j = 0; j < sizeY; j++)
                        {

                            if (maxSizeY < sizeY)
                                simulation.Display(g, i, j, 0);

                            g.DrawRectangle(circuitPenClear, i * CELL_SIZE + 1, j * CELL_SIZE,
                                CELL_SIZE, CELL_SIZE);
                        }
                    }
                }

                if (drawY)
                {
                    for (int i = 0; i < maxSizeX; i++)
                    {
                        for (int j = maxSizeY; j < sizeY; j++)
                        {

                            simulation.Display(g, i, j, 0);

                            g.DrawRectangle(circuitPenClear, i * CELL_SIZE, j * CELL_SIZE + 1,
                                CELL_SIZE, CELL_SIZE);
                        }
                    }
                }
            }
            else
            {
                if (drawX)
                {
                    for (int i = maxSizeX; i < sizeX; i++)
                    {
                        for (int j = 0; j < maxSizeY; j++)
                        {

                            simulation.Display(g, i, j, 0);

                            g.DrawRectangle(circuitPenClear, i * CELL_SIZE + 1, j * CELL_SIZE,
                                CELL_SIZE, CELL_SIZE);
                        }
                    }
                }
            }

        }
        else
        {
           
            if (maxSizeY > sizeY)
            {
                if (drawX)
                {
                    for (int i = sizeX; i < maxSizeX; i++)
                    {
                        for (int j = 0; j < maxSizeY; j++)
                        {

                            g.DrawRectangle(circuitPen, i * CELL_SIZE, j * CELL_SIZE,
                                CELL_SIZE, CELL_SIZE);
                        }
                    }
                }




                if (drawY)
                {
                    for (int i = 0; i < maxSizeX; i++)
                    {
                        for (int j = sizeY; j < maxSizeY; j++)
                        {
                            g.DrawRectangle(circuitPen, i * CELL_SIZE, j * CELL_SIZE,
                                CELL_SIZE, CELL_SIZE);
                        }
                    }
                }
            }
            else if (maxSizeY < sizeY)
            {

                if (drawY)
                {
                    for (int i = 0; i < maxSizeX; i++)
                    {
                        for (int j = maxSizeY; j < sizeY; j++)
                        {

                            simulation.Display(g, i, j, 0);

                            g.DrawRectangle(circuitPenClear, i * CELL_SIZE, j * CELL_SIZE + 1,
                                CELL_SIZE, CELL_SIZE);
                        }
                    }
                }


                if (drawX)
                {
                    for (int i = sizeX; i < maxSizeX; i++)
                    {
                        for (int j = 0; j < maxSizeY; j++)
                        {

                            g.DrawRectangle(circuitPen, i * CELL_SIZE, j * CELL_SIZE,
                            CELL_SIZE, CELL_SIZE);
                        }
                    }
                }


            }
            else
            {
                if (drawX)
                {
                    for (int i = sizeX; i < maxSizeX; i++)
                    {
                        for (int j = 0; j < maxSizeY; j++)
                        {
                            g.DrawRectangle(circuitPen, i * CELL_SIZE, j * CELL_SIZE,
                                CELL_SIZE, CELL_SIZE);
                        }
                    }
                }

            }
        }
    }

    public void Draw(PaintEventArgs e)
    {
        Console.WriteLine("DRAWING1...");

        if (GRID_STATE == GridState.Enable)
        {


            System.Drawing.Graphics g = e.Graphics;


            for (int i = 0; i <= sizeY; i++)
            {
                g.DrawLine(circuitPen, 0, i * CELL_SIZE, sizeX * CELL_SIZE, i * CELL_SIZE);

            }

            for (int j = 0; j <= sizeX; j++)
            {
                g.DrawLine(circuitPen, j * CELL_SIZE, 0, j * CELL_SIZE, sizeY * CELL_SIZE);

            }
        }
    }

    public void Draw(PictureBox pictureBox)
    {
        Console.WriteLine("DRAWING2...");

        if (GRID_STATE == GridState.Enable)
        {
            int maxSizeX = pictureBox.Width / CELL_SIZE;
            int maxSizeY = pictureBox.Height / CELL_SIZE;


            System.Drawing.Graphics g = pictureBox.CreateGraphics();

            g.FillRectangle(cellBrushClear, 0, 0, maxSizeX * CELL_SIZE + 1, maxSizeY * CELL_SIZE + 1);

            for (int i = 0; i <= sizeY; i++)
            {
                g.DrawLine(circuitPen, 0, i * CELL_SIZE, sizeX * CELL_SIZE, i * CELL_SIZE);

            }

            for (int j = 0; j <= sizeX; j++)
            {
                g.DrawLine(circuitPen, j * CELL_SIZE, 0, j * CELL_SIZE, sizeY * CELL_SIZE);

            }
        }
    }

    public bool ComputeBounds(PictureBox pPictureBox, PictureBox pictureBox,
        Simulation simulation, Graphics g)
    {
       

        bool drawX = false;
        bool drawY = false;

        int[,] tabTmp = simulation.Tab;

        int startSizeX = this.sizeX;
        int startSizeY = this.sizeY;

        int previousSizeX = pPictureBox.Width / CELL_SIZE;
        int previousSizeY = pPictureBox.Height / CELL_SIZE;

        int maxSizeX = pictureBox.Width / CELL_SIZE;
        int maxSizeY = pictureBox.Height / CELL_SIZE;


        if (maxSizeX == previousSizeX && maxSizeY == previousSizeY)
        {
            previousSizeX = maxSizeX;
            previousSizeY = maxSizeY;
            return false;
        }
       
        if (this.sizeX == previousSizeX)
        {
            drawX = true;
            this.sizeX = maxSizeX;
        }

        if (this.sizeY == previousSizeY)
        {
            drawY = true;
            this.sizeY = maxSizeY;
        }

        if (maxSizeX < this.sizeX)
        {
            drawX = true;
            this.sizeX = maxSizeX;
        }

        if (maxSizeY < this.sizeY)
        {
            drawY = true;
            this.sizeY = maxSizeY;
        }




        simulation.Tab = new int[sizeY, sizeX];
        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                simulation.Tab[i, j] = 0;
            }
        }
        int y = sizeY < startSizeY ? sizeY : startSizeY;
        int x = sizeX < startSizeX ? sizeX : startSizeX;

        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                simulation.Tab[i, j] = tabTmp[i, j];
            }
        }

        Draw(pictureBox);

       // Draw(pictureBox, g, simulation, startSizeX, startSizeY, drawX, drawY);


        return true;
    }

    public void RenderGridAndRefresh(Graphics g, PictureBox pictureBox)
    {

        int maxSizeX = pictureBox.Width / CELL_SIZE;
        int maxSizeY = pictureBox.Height / CELL_SIZE;


        System.Drawing.Pen circuitPen = new System.Drawing.Pen(Color.Black, 1);

       g.FillRectangle(cellBrushClear, 0, 0, maxSizeX * CELL_SIZE + 1, maxSizeY * CELL_SIZE + 1 );

        if (GRID_STATE == GridState.Enable)
        {
            for (int i = 0; i <= sizeY; i++)
            {
               g.DrawLine(circuitPen, 0, i * CELL_SIZE, sizeX * CELL_SIZE, i * CELL_SIZE);

            }

            for (int j = 0; j <= sizeX; j++)
            {
                g.DrawLine(circuitPen, j * CELL_SIZE, 0, j * CELL_SIZE, sizeY * CELL_SIZE);

            }
        }
    }

    public void SetNewCellSizeAndDraw(Graphics gg, PictureBox pictureBox, Simulation simulation)
    {
        Graphics g = pictureBox.CreateGraphics();

        int maxSizeX = pictureBox.Width / CELL_SIZE + 1;
        int maxSizeY = pictureBox.Height / CELL_SIZE + 1;
        g.FillRectangle(cellBrushClear, 0, 0, maxSizeX * CELL_SIZE + 1, maxSizeY * CELL_SIZE + 1);

        int[,] tabTmp = simulation.Tab;

        int startSizeX = this.sizeX;
        int startSizeY = this.sizeY;


        this.sizeX = pictureBox.Width / CELL_SIZE;
        this.sizeY = pictureBox.Height / CELL_SIZE;

        simulation.Tab = new int[sizeY, sizeX];

        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                simulation.Tab[i, j] = 0;
            }
        }

        int y = sizeY < startSizeY ? sizeY : startSizeY;
        int x = sizeX < startSizeX ? sizeX : startSizeX;

        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                simulation.Tab[i, j] = tabTmp[i, j];
            }
        }

        if (GRID_STATE == GridState.Enable)
        {
            Draw(pictureBox);
        }

        simulation.Display(g);
    }

}

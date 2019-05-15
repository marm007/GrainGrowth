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
    }


    public void Draw(PictureBox pictureBox)
    {

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
            this.sizeX = maxSizeX;
        }

        if (this.sizeY == previousSizeY)
        {
            this.sizeY = maxSizeY;
        }

        if (maxSizeX < this.sizeX)
        {
            this.sizeX = maxSizeX;
        }

        if (maxSizeY < this.sizeY)
        {
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

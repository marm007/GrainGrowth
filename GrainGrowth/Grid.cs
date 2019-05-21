using System;
using System.Drawing;
using System.Windows.Forms;
using static Config;

public class Grid
{

    private System.Drawing.Pen circuitPen = new System.Drawing.Pen(Color.Black, 1);
    private System.Drawing.Pen circuitPenClear = new System.Drawing.Pen(SystemColors.Control, 1);
    private System.Drawing.SolidBrush cellBrushClear = new System.Drawing.SolidBrush(SystemColors.Control);


   

    public Grid()
	{
    }

    public void Draw(Graphics g)
    {

        if (GRID_STATE == GridState.Enable)
        {
            g.FillRectangle(cellBrushClear, 0, 0, SIZE_X * CELL_SIZE + 1, SIZE_Y * CELL_SIZE + 1);

            for (int i = 0; i <= SIZE_Y; i++)
            {
                g.DrawLine(circuitPen, 0, i * CELL_SIZE, SIZE_X * CELL_SIZE, i * CELL_SIZE);

            }

            for (int j = 0; j <= SIZE_X; j++)
            {
                g.DrawLine(circuitPen, j * CELL_SIZE, 0, j * CELL_SIZE, SIZE_Y * CELL_SIZE);

            }
        }
    }

    public void Draw(PictureBox pictureBox)
    {

        if (GRID_STATE == GridState.Enable)
        {
            int maxSizeX = pictureBox.Width / CELL_SIZE;
            int maxSizeY = pictureBox.Height / CELL_SIZE;


            System.Drawing.Graphics g = pictureBox.CreateGraphics();

            g.FillRectangle(cellBrushClear, 0, 0, maxSizeX * CELL_SIZE + 1, maxSizeY * CELL_SIZE + 1);

            for (int i = 0; i <= SIZE_Y; i++)
            {
                g.DrawLine(circuitPen, 0, i * CELL_SIZE, SIZE_X * CELL_SIZE, i * CELL_SIZE);

            }

            for (int j = 0; j <= SIZE_X; j++)
            {
                g.DrawLine(circuitPen, j * CELL_SIZE, 0, j * CELL_SIZE, SIZE_Y * CELL_SIZE);

            }
        }
    }

    public bool ComputeBounds(PictureBox pPictureBox, PictureBox pictureBox,
        Simulation simulation, Graphics g)
    {
       
        Grain[,] tabTmp = simulation.Tab;

        int startSizeX = SIZE_X;
        int startSizeY = SIZE_Y;

        int previousSizeX = pPictureBox.Width / CELL_SIZE;
        int previousSizeY = pPictureBox.Height / CELL_SIZE;

        int maxSizeX = pictureBox.Width / CELL_SIZE;
        int maxSizeY = pictureBox.Height / CELL_SIZE;

        int sizeX = SIZE_X;
        int sizeY = SIZE_Y;

        if (maxSizeX == previousSizeX && maxSizeY == previousSizeY)
        {
            return false;
        }
       
        if (SIZE_X == previousSizeX)
        {
            sizeX = maxSizeX;
        }

        if (SIZE_Y == previousSizeY)
        {
            sizeY = maxSizeY;
        }

        if (maxSizeX < SIZE_X)
        {
            sizeX = maxSizeX;
        }

        if (maxSizeY < SIZE_Y)
        {
            sizeY = maxSizeY;
        }

        // setting new sizes

        SET_SIZES(sizeX, sizeY);

        // --------

        simulation.Tab = new Grain[SIZE_Y, SIZE_X];
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                simulation.Tab[i, j] = new Grain(j, i, 0);
            }
        }
        int y = SIZE_Y < startSizeY ? SIZE_Y : startSizeY;
        int x = SIZE_X < startSizeX ? SIZE_X : startSizeX;

        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                simulation.Tab[i, j].State = tabTmp[i, j].State;
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
            for (int i = 0; i <= SIZE_Y; i++)
            {
               g.DrawLine(circuitPen, 0, i * CELL_SIZE, SIZE_X * CELL_SIZE, i * CELL_SIZE);

            }

            for (int j = 0; j <= SIZE_X; j++)
            {
                g.DrawLine(circuitPen, j * CELL_SIZE, 0, j * CELL_SIZE, SIZE_Y * CELL_SIZE);

            }
        }
    }

    public void SetNewCellSizeAndDraw(Graphics gg, PictureBox pictureBox, Simulation simulation)
    {
        Graphics g = pictureBox.CreateGraphics();

        int maxSizeX = pictureBox.Width / CELL_SIZE + 1;
        int maxSizeY = pictureBox.Height / CELL_SIZE + 1;
        g.FillRectangle(cellBrushClear, 0, 0, maxSizeX * CELL_SIZE + 1, maxSizeY * CELL_SIZE + 1);

        Grain[,] tabTmp = simulation.Tab;

        int startSizeX = SIZE_X;
        int startSizeY = SIZE_Y;


        // setting new sizes

        SET_SIZES(pictureBox.Width / CELL_SIZE, pictureBox.Height / CELL_SIZE);

        // --------


        simulation.Tab = new Grain[SIZE_Y, SIZE_X];

        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                simulation.Tab[i, j] = new Grain(j, i, 0);
            }
        }

        int y = SIZE_Y < startSizeY ? SIZE_Y : startSizeY;
        int x = SIZE_X < startSizeX ? SIZE_X : startSizeX;

        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                simulation.Tab[i, j].State = tabTmp[i, j].State;
            }
        }

        if (GRID_STATE == GridState.Enable)
        {
            Draw(pictureBox);
        }

        simulation.Display(g);
    }

}

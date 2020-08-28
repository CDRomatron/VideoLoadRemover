state("Livesplit")
{
	
}

init
{
	vars.loadflag = false;
	vars.bmp = new System.Drawing.Bitmap(100,100);
	vars.savedbmp = new System.Drawing.Bitmap("VideoLoadRemover/saved.bmp");
	
	string[] invars = File.ReadAllLines("VideoLoadRemover/settings.ini");

	vars.xc = Convert.ToInt32(invars[0].Split('=')[1]);
	vars.yc = Convert.ToInt32(invars[1].Split('=')[1]);
	vars.w = Convert.ToInt32(invars[2].Split('=')[1]);
	vars.h = Convert.ToInt32(invars[3].Split('=')[1]);
	vars.lst = Convert.ToInt32(invars[4].Split('=')[1]);
	refreshRate = Convert.ToInt32(invars[5].Split('=')[1]);
}

update
{
	try
	{
		System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(vars.w, vars.h);
		using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap))
		{
			g.CopyFromScreen(vars.xc, vars.yc, 0, 0, bitmap.Size);
		}

		vars.bmp = (System.Drawing.Bitmap)bitmap.Clone();

		try
		{
			System.Drawing.Bitmap bm1 = new System.Drawing.Bitmap(vars.bmp, new System.Drawing.Size(vars.bmp.Width / 2, vars.bmp.Height / 2));
			System.Drawing.Bitmap bm2 = new System.Drawing.Bitmap(vars.savedbmp, new System.Drawing.Size(vars.savedbmp.Width / 2, vars.savedbmp.Height / 2));

			List<System.Drawing.Color> histo1 = new List<System.Drawing.Color>();
			for (int x = 0; x < bm1.Width; x++)
            {
                for (int y = 0; y < bm1.Height; y++)
                {
                    // Get pixel color 
                    System.Drawing.Color c = bm1.GetPixel(x, y);
                    // If it exists in our 'histogram' increment the corresponding value, or add new
                    histo1.Add(c);
                }
            }
			
			List<System.Drawing.Color> histo2 = new List<System.Drawing.Color>();
			for (int x = 0; x < bm2.Width; x++)
            {
                for (int y = 0; y < bm2.Height; y++)
                {
                    // Get pixel color 
                    System.Drawing.Color c = bm2.GetPixel(x, y);
                    // If it exists in our 'histogram' increment the corresponding value, or add new
                    histo2.Add(c);
                }
            }
			
			float val1 = 0;
			
			for (int i = 0; i < histo1.Count; i++)
            {
                val1 += Math.Abs((float)(histo1.ElementAt(i).ToArgb() - histo2.ElementAt(i).ToArgb()));
            }
			

			val1 = (val1 + 1) / 10000000;


			vars.loadflag = val1 < vars.lst;
		}
		catch (Exception ex)
		{
			//print(ex.Message);
		}
	}
	catch (Exception exp)
	{
		Console.WriteLine("Skipping...");
	}
}

isLoading
{
	if(vars.loadflag)
	{
		return true;
	}
	else
	{
		return false;
	}
}
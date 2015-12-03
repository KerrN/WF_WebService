using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Hosting;


// Maths is dumb, dont question the code, it works ... most of the time

namespace WF_webService
{
    public class ImageProccessor
    {
        Bitmap image = null;
        //public static Dictionary<int, int[]> splitted = new Dictionary<int, int[]>();
        List<Color> keys = new List<Color>();
        int width = 0;
        int height = 0;
        Color highlight = Color.FromArgb((-39424 >> 16) & 0XFF, (-39424 >> 8) & 0XFF, (-39424) & 0XFF);

        Color[] img;
        List<Sampled> samples = new List<Sampled>();
        Color backgroundColor = Color.White;

        public string GenerateImage(string path, string colorPath, string roomName, List<Waypoint> sequence,int xanchor, int yanchor, double scale)
        {
            int roomNumber = -1;
            try
            {
                roomNumber = Int32.Parse(roomName.Substring(roomName.Length - 3));
                roomNumber = roomNumber & 0xFF;
            }
            catch (Exception ex)
            {
                return "cant parse room number "+roomName;
            }

            long timeStamp = DateTime.Now.Ticks;
            long last = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if(!path.Contains("/Img/"))
                path = "/Img/"+path;
            try
            {
                image = (Bitmap)Image.FromFile(HostingEnvironment.ApplicationPhysicalPath+path+".png" , false);
            }
            catch (Exception ex)
            {
                return "image not found : " + HostingEnvironment.ApplicationPhysicalPath + path+".png";
            }

            width = image.Width;
            height = image.Height;
            img = getRGB();
            backgroundColor = img[0];

            long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            last = now;
            sampleColors();
            for (int i = 0; i < keys.Count; i++)
            {
                sampleRooms(keys.ElementAt(i));
            }
            now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            Console.Write("Sampled in "+(now-last) +" millis");
            last = now;
            color(roomNumber);
            now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            Console.Write("Colored in "+(now-last)+" millis");
            drawPoints(sequence,xanchor,yanchor,scale);
            string serverDest = HostingEnvironment.ApplicationPhysicalPath + path + timeStamp.ToString()+"_ROOM_"+roomName+".png";
            if(print(serverDest))
            {
                return serverDest;
            }
            return "Error: "+serverDest;
        }

        bool print(String ServerDest)
        {
           
                image.Save(ServerDest, ImageFormat.Png);
                return true;
      
        }
       
        // i hate maths

        void drawPoints(List<Waypoint> sequence,int xanchor, int yanchor, double scale)
        {

            int size = sequence.Count;
            for(int i = 0; i < size;i++)
                    {
                        int y =(int)( (sequence.ElementAt(i).coordY+yanchor) *scale);
                        int x = (int)( (sequence.ElementAt(i).coordX+xanchor) *scale);
                        image.SetPixel(x, y, highlight);
                        
                           if(i==0)
                                DrawFilledCircle(x,y,18);
                              
                          if(i==size)
                               DrawFilledCircle(x,y,18);
                                
                                
                        
                        double gradient =0;
                        double steps = 0;
                        
                        if(i<size-1)
                        {
                            double pixsteps = ((50 * scale));
                            double xd =(sequence.ElementAt(i+1).coordX-sequence.ElementAt(i).coordX);
                          // if(xd>0){
                            double yd = (sequence.ElementAt(i+1).coordY-sequence.ElementAt(i).coordY);
                            if(xd==0)
                                xd=1;
                            gradient = (yd/xd);
                           
                           //}
                          
                            //System.out.println(gradient);
                           
                            if(sequence.ElementAt(i+1).coordX != sequence.ElementAt(i).coordX)
                            {
                            	
                                //steps = (Point2D.distance(sequence.ElementAt(i).coordX, sequence.ElementAt(i).coordY, sequence.ElementAt(i).coordX, sequence.ElementAt(i).coordY));
                                steps = Math.Abs(sequence.ElementAt(i+1).coordX-sequence.ElementAt(i).coordX);
                                //System.out.println("steps " +steps);
                               // System.out.println("x2 "+ sequence.ElementAt(i).coordX+"- x1 "+sequence.ElementAt(i).coordX+" = "+steps);
                              int numSteps =(int)(steps/pixsteps);
                                 for(double s = 0; s <=numSteps ; s ++)
                                {
                                   // System.out.println("*************************");
                                      for(double ss = 0; ss<=steps;ss+=0.1)
                                      {
                                         
                                        //double newStep = (pixsteps*gradient);
                                          double xxx = ((int) ((ss)+(ss*Math.Abs(gradient))));
                                          //  int found = 0;
                                          // System.out.println(xxx);
                                          
                                              if((int)xxx%pixsteps==0){
                                            
                                            //System.out.println("gradient "+gradient+" ,x diff "+ss +" distance "+xxx);
                                            double xstep = ss;
                                          
                                           
                                            
                                            //System.out.println("gradient: "+gradient+" ss "+ss+" xstep "+xstep);
                                                if(xd>0)
                                              DrawFilledCircle((x+(int)((xstep*scale)))-1,(int)((y+(xstep*gradient*scale))),9);
                                                else
                                                    DrawFilledCircle((x-(int)((xstep*scale)))-1,(int)((y+(xstep*-gradient*scale))),9);
                                           
                                              //System.out.println("x: "+(x+(int)((ss*scale))-1));
                                             // System.out.println("y: "+(int)((y+(ss*gradient))));
                                          }
                                           }
                                      
                                }
                             }   
                            else
                              if(sequence.ElementAt(i+1).coordY != sequence.ElementAt(i).coordY)
                            {
                                steps = Math.Abs(sequence.ElementAt(i+1).coordY-sequence.ElementAt(i).coordY);
                                bool neg = (sequence.ElementAt(i + 1).coordY - sequence.ElementAt(i).coordY < 0);
                                //System.out.println("y2 "+ sequence.ElementAt(i).coordY+"- y1 "+sequence.ElementAt(i).coordY+" = "+steps);
                                 for(int s = 0; s < steps/10; s ++)
                                {
                                      for(int ss = 0; ss<steps;ss++)
                                      {
                                          if(ss%((pixsteps))==1)
                                          {
                                              if(neg)
                                              DrawFilledCircle((x),(int)(((y)))+(int)-(ss*scale)-1,9);
                                              else
                                              DrawFilledCircle((x), (int)(((y))) + (int)(ss * scale) - 1, 9);
                                          }
                                      }
                                }
                             } 
                         }
                           
                        
                    }
        }

        void DrawFilledCircle(int x0, int y0, int radius)
        {
            int x = radius;
            int y = 0;
            int xChange = 1 - (radius << 1);
            int yChange = 0;
            int radiusError = 0;

            while (x >= y)
            {
                for (int i = x0 - x; i <= x0 + x; i++)
                {
                    image.SetPixel(i,(y0+y),highlight);
                    image.SetPixel(i,(y0-y),highlight);
                }
                for (int i = x0 - y; i <= x0 + y; i++)
                {
                    image.SetPixel(i,(y0+x),highlight);
                    image.SetPixel(i,(y0-x),highlight);
                }

                y++;
                radiusError += yChange;
                yChange += 2;
                if (((radiusError << 1) + xChange) > 0)
                {
                    x--;
                    radiusError += xChange;
                    xChange += 2;
                }
            }
        }
        public class MyException : Exception
        {
            public MyException() { }
            public MyException(string message) : base(message) { }

        }

        void color(int roomNumber)
        {
            Sampled output = null;
            foreach (Sampled s in samples)
            {
                if (s.getColor().R == roomNumber)
                {
                    output = s;
                }
            }
            if (output != null)
            {

                try
                {


                    int topbounds = output.getTop();
                    int leftbounds = output.getLeft();
                    int rightbounds = output.getRight();
                    int bottombounds = output.getBottom();

                    int tempwidth = rightbounds - leftbounds;
                    int tempheight = bottombounds - topbounds;
                    //throw new MyException("left: " + output.getLeft() + " ,right: " + output.getRight() + " ,top: " + output.getTop() + " ,bottom: " + output.getBottom()+" ,widthbounds: "+tempwidth+" ,heightbounds: "+tempheight+"\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");


                    for (int y = 0; y < tempheight; y++)
                    {
                        for (int x = 0; x < tempwidth; x++)
                        {
                            if (img[width * (y + topbounds) + (x + leftbounds)] == output.getColor())
                            {
                                //img[width * (y + topbounds) + (x + rightbounds)] = -39424;


                                image.SetPixel(x + leftbounds, y + topbounds, highlight);
                            }

                        }
                    }
                }
                catch (Exception e)
                {
                    throw new ApplicationException(output.toString(), e);
                }
            }
        }

        void sampleRooms(Color color)
        {

            int minX = 0;
            int maxX = width;
            int minY = 0;
            int maxY = height;
            int lowestX = width;
            int lowestY = height;
            int heighestX = 0;
            int heighestY = 0;






            for (int sy = 0; sy < height; sy++)
            {


                for (int sx = 0; sx < width; sx++)
                {
                    if (img[(width * sy) + sx] == color && sx < maxX && maxX < width && sx < lowestX)
                    {
                        minX = sx;
                        if (sx < lowestX)
                            lowestX = sx;
                    }
                    if (img[(width * sy) + sx] == color && sx > minX && sx > heighestX)
                    {
                        maxX = sx;
                        heighestX = sx;
                    }

                    if (img[(width * sy) + sx] == color && sy > minY && maxY < height && sy < lowestY)
                    {
                        minY = sy;
                        if (sy < lowestY)
                            lowestY = sy;
                    }
                    if (img[(width * sy) + sx] == color && sy > minY && sy > heighestY)
                    {
                        maxY = sy;
                        heighestY = sy;
                    }


                }

            }

           
                Sampled s = new Sampled(color, lowestX, lowestY, heighestX, heighestY);
                foreach(Sampled te in samples)
                    if(te.getColor()==s.getColor())
                        samples.Remove(te);
                samples.Add(s);
            
        }

        void sampleColors()
        {
            int sw = 8;
            int sh = 8;

            for (int sy = 0; sy < Math.Floor((double)height / sh); sy++)
            {


                for (int sx = 0; sx < Math.Floor((double)width / sw); sx++)
                {
                    if (!keys.Contains(img[(width * (sy * 8)) + (sx * 8) + 8]))
                    {
                        keys.Add(img[(width * (sy * 8)) + (sx * 8) + 8]);
                    }
                    if (!keys.Contains(img[(width * (sy * 8)) + (sx * 8)]))
                    {
                        keys.Add(img[(width * (sy * 8)) + (sx * 8)]);
                    }

                    if (!keys.Contains(img[(width * (sy * 8)) + (sx * 8) + 8]))
                    {
                        keys.Add(img[(width * (sy * 8)) + (sx * 8) + 8]);
                    }
                    if (!keys.Contains(img[(width * (sy * 8)) + (sx * 8)]))
                    {
                        keys.Add(img[(width * (sy * 8)) + (sx * 8)]);
                    }


                }

            }


        }

        Color[] getRGB()
        {
            Color[] pixels = new Color[width * height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    pixels[x + (y * width)] = image.GetPixel(x, y);
                }
            }
            return pixels;

        }
    



    }

    public class Sampled
    {

        private int left;
        private Color color;
        private int right;
        private int top;
        private int bottom;

        public Sampled(Color color, int left, int top, int right, int bottom)
        {
            this.color = color;
            this.left = left;
            this.right = right;
            this.top = top;
            this.bottom = bottom;
        }

        public int getLeft()
        {
            return left;
        }

        public Color getColor()
        {
            return color;
        }

        public int getRight()
        {
            return right;
        }

        public int getTop()
        {
            return top;
        }

        public int getBottom()
        {
            return bottom;
        }

        public void setLeft(int left)
        {
            this.left = left;
        }

        public void setColor(Color color)
        {
            this.color = color;
        }

        public void setRight(int right)
        {
            this.right = right;
        }

        public void setTop(int top)
        {
            this.top = top;
        }

        public void setBottom(int bottom)
        {
            this.bottom = bottom;
        }
        public string toString()
        {
            return "color: "+color.R+" left: "+left+" right: "+right+" up: "+top+" down: "+bottom;
        }
    }
}
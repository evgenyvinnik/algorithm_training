using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teams
{
    class Building
    {
        public int height { get; set; }
        public int width { get; set; }

        public int leftX { get; set; }

        public  bool belongsToBuilding(Coordinate coordinate)
        {
            bool res;

            if (coordinate.X >= leftX && coordinate.X <= leftX + width
                && coordinate.Y >= 0 && coordinate.X <= leftX + width)

            return res;
        }
    }

    class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Building> buildings = new List<Building>();

            //initialize them

            buildings.Add(new Building() { height = 2, width = 3, leftX = 0 });
            buildings.Add(new Building() { height = 5, width = 8, leftX = 2 });
            buildings.Add(new Building() { height = 2, width = 2, leftX = 4 });
            buildings.Add(new Building() { height = 3, width = 5, leftX = 7 });
            buildings.Add(new Building() { height = 1, width = 4, leftX = 11 });


            //  AAAAAAAA
            //? ?AAAAAAA
            //^.^AAAAAARRRRR
            //.S?A##AAARRRRR
            //.SSA##AAARRRRTTTT

            List<Coordinate> coordinates = getSkylineCoordinates(buildings);
            //Console.WriteLine($"{frontArea(buildings)}");
        }

        enum Direction
        {
            UP,
            DOWN,
            LEFT
        }

        static List<Coordinate> getSkylineCoordinates(List<Building> buildings)
        {
            List<Coordinate> res = new List<Coordinate>();

            Direction direction = Direction.UP;

            int x = 0;
            int y = 0;

            //for(int i = 0; i < buildings.Count; i++)
            int i = 0;
            while (true)
            {
                //int tempX = buildings[i].leftX;
                //int tempHeight = buildings[i].height;
                //int tempWidth = buildings[i].width;

                switch(direction)
                {
                    case Direction.UP:
                        Coordinate coordinate = new Coordinate() { X = x, Y = y +1 };
                        if(buildings[i].belongsToBuilding(coordinate))
                        {
                            x = coordinate.X;
                            y = coordinate.Y;
                        }
                        else
                        {
                            // check next building
                            if(buildings[i+1].belongsToBuilding(coordinate))
                            {
                                i++;
                            }
                            else
                            {
                                direction = Direction.LEFT;
                                res.Add(new Coordinate() { X = x, Y = y });
                            }
                        }
                        break;
                    case Direction.LEFT:
                        break;
                    case Direction.DOWN:
                        break;
                }
            }

            //1. assumption list of buildings sorted by leftX coordinate;
            //2. we go from left to right thru all the buildings in the list
            //3. we calculate current x, y
            //4. add coordinates when skyline goes down or up.



            return res;
        }




        //static int frontArea(List<Building> buildings)
        //{
        //    int res = 0;

        //    return res;
        //}
    }
}

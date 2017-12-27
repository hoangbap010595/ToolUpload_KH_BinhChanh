using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunfrogShirts
{
    public class ObjColor
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public ObjColor()
        {
           
        }
        public List<ObjColor> getObjColorGuysTeeAndLadiesTee()
        {
            List<ObjColor> lsObjColor = new List<ObjColor>();
            lsObjColor.Add(new ObjColor { Name = "White", Value = "255, 255, 255" });
            lsObjColor.Add(new ObjColor { Name = "Sports Grey", Value = "128,128,128" });
            lsObjColor.Add(new ObjColor { Name = "Dark Grey", Value = "169,169,169" });
            lsObjColor.Add(new ObjColor { Name = "Brown", Value = "104, 56, 26" });
            lsObjColor.Add(new ObjColor { Name = "Light Pink", Value = "255, 187, 211" });
            lsObjColor.Add(new ObjColor { Name = "Hot Pink", Value = "251, 35, 96" });
            lsObjColor.Add(new ObjColor { Name = "Red", Value = "193, 13, 15" });
            lsObjColor.Add(new ObjColor { Name = "Orange", Value = "255, 62, 4" });
            lsObjColor.Add(new ObjColor { Name = "Yellow", Value = "255, 195, 34" });
            lsObjColor.Add(new ObjColor { Name = "Green", Value = "57, 149, 72" });
            lsObjColor.Add(new ObjColor { Name = "Forest", Value = "45, 57, 28" });
            lsObjColor.Add(new ObjColor { Name = "Light Blue", Value = "195, 226, 241" });
            lsObjColor.Add(new ObjColor { Name = "Royal Blue", Value = "50, 78, 148" });
            lsObjColor.Add(new ObjColor { Name = "Navy Blue", Value = "3, 3, 31" });
            lsObjColor.Add(new ObjColor { Name = "Purple", Value = "67, 49, 97" });
            lsObjColor.Add(new ObjColor { Name = "Black", Value = "0, 0, 0" });
            lsObjColor.Add(new ObjColor { Name = "Maroon", Value = "71, 0, 9" });
            return lsObjColor;
        }
        public List<ObjColor> getObjColorYouthTee()
        {
            List<ObjColor> lsObjColor = new List<ObjColor>();
            lsObjColor.Add(new ObjColor { Name = "White", Value = "255, 255, 255" });
            lsObjColor.Add(new ObjColor { Name = "Sports Grey", Value = "128,128,128" });
            lsObjColor.Add(new ObjColor { Name = "Dark Grey", Value = "169,169,169" });
            lsObjColor.Add(new ObjColor { Name = "Black", Value = "0, 0, 0" });
            lsObjColor.Add(new ObjColor { Name = "Navy Blue", Value = "3, 3, 31" });
            lsObjColor.Add(new ObjColor { Name = "Red", Value = "193, 13, 15" });
            lsObjColor.Add(new ObjColor { Name = "Royal Blue", Value = "50, 78, 148" });        
            return lsObjColor;
        }
        public List<ObjColor> getObjColorHoodie()
        {
            List<ObjColor> lsObjColor = new List<ObjColor>();
            lsObjColor.Add(new ObjColor { Name = "White", Value = "255, 255, 255" });
            lsObjColor.Add(new ObjColor { Name = "Sports Grey", Value = "128,128,128" });
            lsObjColor.Add(new ObjColor { Name = "Maroon", Value = "71, 0, 9" });
            lsObjColor.Add(new ObjColor { Name = "Red", Value = "193, 13, 15" });
            lsObjColor.Add(new ObjColor { Name = "Royal Blue", Value = "50, 78, 148" });
            lsObjColor.Add(new ObjColor { Name = "Navy Blue", Value = "3, 3, 31" });
            lsObjColor.Add(new ObjColor { Name = "Charcoal", Value = "68, 66, 68" });
            lsObjColor.Add(new ObjColor { Name = "Forest", Value = "45, 57, 28" });
            lsObjColor.Add(new ObjColor { Name = "Green", Value = "57, 149, 72" });
            lsObjColor.Add(new ObjColor { Name = "Black", Value = "0, 0, 0" });

            return lsObjColor;
        }
        public List<ObjColor> getObjColorSweatSirt()
        {
            List<ObjColor> lsObjColor = new List<ObjColor>();
            lsObjColor.Add(new ObjColor { Name = "White", Value = "255, 255, 255" });
            lsObjColor.Add(new ObjColor { Name = "Black", Value = "0, 0, 0" });
            lsObjColor.Add(new ObjColor { Name = "Forest", Value = "45, 57, 28" });
            lsObjColor.Add(new ObjColor { Name = "Navy Blue", Value = "3, 3, 31" });
            lsObjColor.Add(new ObjColor { Name = "Red", Value = "193, 13, 15" });
            lsObjColor.Add(new ObjColor { Name = "Royal Blue", Value = "50, 78, 148" });
            lsObjColor.Add(new ObjColor { Name = "Sports Grey", Value = "128,128,128" });
            return lsObjColor;
        }
        public List<ObjColor> getObjColorGuysVNeck()
        {
            List<ObjColor> lsObjColor = new List<ObjColor>();
            lsObjColor.Add(new ObjColor { Name = "White", Value = "255, 255, 255" });
            lsObjColor.Add(new ObjColor { Name = "Black", Value = "0, 0, 0" });
            lsObjColor.Add(new ObjColor { Name = "Navy Blue", Value = "3, 3, 31" });
            lsObjColor.Add(new ObjColor { Name = "Red", Value = "193, 13, 15" });
            lsObjColor.Add(new ObjColor { Name = "Sports Grey", Value = "128,128,128" });

            return lsObjColor;
        }
        public List<ObjColor> getObjColorLadiesVNeck()
        {
            List<ObjColor> lsObjColor = new List<ObjColor>();
            lsObjColor.Add(new ObjColor { Name = "White", Value = "255, 255, 255" });
            lsObjColor.Add(new ObjColor { Name = "Black", Value = "0, 0, 0" });
            lsObjColor.Add(new ObjColor { Name = "Navy Blue", Value = "3, 3, 31" });
            lsObjColor.Add(new ObjColor { Name = "Red", Value = "193, 13, 15" });
            lsObjColor.Add(new ObjColor { Name = "Royal Blue", Value = "50, 78, 148" });
            lsObjColor.Add(new ObjColor { Name = "Dark Grey", Value = "169,169,169" });
            lsObjColor.Add(new ObjColor { Name = "Purple", Value = "67, 49, 97" });

            return lsObjColor;
        }
        public List<ObjColor> getObjColorUnisex()
        {
            List<ObjColor> lsObjColor = new List<ObjColor>();
            lsObjColor.Add(new ObjColor { Name = "White", Value = "255, 255, 255" });
            lsObjColor.Add(new ObjColor { Name = "Black", Value = "0, 0, 0" });
            lsObjColor.Add(new ObjColor { Name = "Navy Blue", Value = "3, 3, 31" });
            lsObjColor.Add(new ObjColor { Name = "Red", Value = "193, 13, 15" });
            lsObjColor.Add(new ObjColor { Name = "Royal Blue", Value = "50, 78, 148" });
            lsObjColor.Add(new ObjColor { Name = "Sports Grey", Value = "128,128,128" });

            return lsObjColor;
        }
        public List<ObjColor> getObjColorHat()
        {
            List<ObjColor> lsObjColor = new List<ObjColor>();
            lsObjColor.Add(new ObjColor { Name = "Black", Value = "0, 0, 0" });
            lsObjColor.Add(new ObjColor { Name = "Green", Value = "57, 149, 72" });
            lsObjColor.Add(new ObjColor { Name = "Navy Blue", Value = "3, 3, 31" });
            lsObjColor.Add(new ObjColor { Name = "Red", Value = "193, 13, 15" });
            lsObjColor.Add(new ObjColor { Name = "Royal Blue", Value = "50, 78, 148" });

            return lsObjColor;
        }
        public List<ObjColor> getObjColorTruckerCap()
        {
            List<ObjColor> lsObjColor = new List<ObjColor>();
            lsObjColor.Add(new ObjColor { Name = "White", Value = "255, 255, 255" });
            lsObjColor.Add(new ObjColor { Name = "Black", Value = "0, 0, 0" });
            lsObjColor.Add(new ObjColor { Name = "Navy Blue", Value = "3, 3, 31" });
            lsObjColor.Add(new ObjColor { Name = "Dark Grey", Value = "169,169,169" });
            return lsObjColor;
        }
        public List<ObjColor> getObjColorBlack()
        {
            List<ObjColor> lsObjColor = new List<ObjColor>();
            lsObjColor.Add(new ObjColor { Name = "Black", Value = "0, 0, 0" });
            return lsObjColor;
        }
        public List<ObjColor> getObjColorWhite()
        {
            List<ObjColor> lsObjColor = new List<ObjColor>();
            lsObjColor.Add(new ObjColor { Name = "White", Value = "255, 255, 255" });
            return lsObjColor;
        }
    }
}

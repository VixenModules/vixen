﻿namespace VixenModules.App.DisplayPreview.Model.Shapes
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class Line : IShape
    {
        public ShapeType ShapeType
        {
            get
            {
                return ShapeType.Line;
            }
        }
    }
}

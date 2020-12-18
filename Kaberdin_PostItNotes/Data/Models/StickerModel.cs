using System;
using System.ComponentModel.DataAnnotations;

namespace Kaberdin_PostItNotes.Data.Models
{
    public class StickerModel
    {
        public enum StickerColor
        {
            Blue,Pink,Yellow
        }

        [Key]
        public int StickerId { get; set; }
        public float? PositionX { get; set; }
        public float? PositionY { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public StickerColor? Color { get; set; }
        public String? Content { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public int? PositionX { get; set; }
        public int? PositionY { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public StickerColor? Color { get; set; }
        public String? Content { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace Bazaro.Data.Models.References
{
    public record class FolderEntryReference
    {
        [Required]
        public Folder Folder { get; set; }
        public int FolderId { get; set; }

        [Required]
        public Entry Entry { get; set; }
        public int EntryId { get; set; }
    }
}

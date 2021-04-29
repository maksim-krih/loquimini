using Loquimini.Model.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loquimini.Model.Core
{
    public class CoreEntity<TKey>: ICoreEntity<TKey> where TKey: IEquatable<TKey>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public TKey Id { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Like.Domain.Entities
{
    [Table("tbl_blog", Schema = "public")]
    public class Blog : Base
    {
        [Key]
        [Column("id_blog")]
        public int Id { get; set; }

        [Column("tx_posts")]
        public string Posts { get; set; }


        [Column("qty_like")]
        public int QuantityLike { get; set; }

        [Column("dt_insert")]
        public DateTime DtInsert { get; set; } = DateTime.Now;

        [Column("dt_update")]
        public DateTime DtUpdate { get; set; } = DateTime.Now;
    }
}

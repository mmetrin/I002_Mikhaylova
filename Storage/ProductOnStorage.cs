//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Storage
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductOnStorage
    {
        public int IdProductOnStorage { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    
        public virtual Product Product { get; set; }
    }
}

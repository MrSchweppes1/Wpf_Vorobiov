//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wpf_Vorobiov
{
    using System;
    using System.Collections.Generic;
    
    public partial class auth
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public int role { get; set; }
    
        public virtual roles roles { get; set; }
        public virtual users users { get; set; }
    }
}

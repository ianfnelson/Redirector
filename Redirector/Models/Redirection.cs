using System;

namespace Redirector.Models
{
    public class Redirection
    {
        public virtual int Id { get; set; }
        
        public virtual string Path { get; set; }
        
        public virtual string RedirectUrl { get; set; }
        
        public virtual int HitCount { get; set; }
        
        public virtual DateTime Created { get; set; }
        
        public virtual DateTime? LastHit { get; set; }
        
        public virtual bool IsPermanent { get; set; }
        
        public virtual bool Checked { get; set; }
    }
}
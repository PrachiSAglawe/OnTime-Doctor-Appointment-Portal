using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Dtos
{
    public class CheckBoxDto
    {
        public Int64 Value {  get; set; }
        public string Text {  get; set; }
        public bool IsSelected {  get; set; }
    }
}

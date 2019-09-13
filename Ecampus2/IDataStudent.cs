using System.Collections.Generic;
using System.Windows.Controls;

namespace Ecampus2
{
    public interface IDataStudent
    {
        List<string> MyAccount();
        List<Expander> Lessons();
        void Rating();
        string WiFI();
        string Teachers();
        void Placement(string search);
        void Schedule();
    }
}

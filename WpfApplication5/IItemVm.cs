﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication5
{
    public interface IItemVm
    {
        string Header { get; set; }
        bool Favorite { get; set; }
        long Id { get; set; }
    }
}

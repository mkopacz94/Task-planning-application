using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskPlanningApp.View;
using TaskPlanningApp.ViewModel;

namespace TaskPlanningApp.Model
{
    public static class WindowBuilder
    {
        public static Window BuildWindow(WorkspaceViewModel viewModel)
        {
            Window window = new TemplateDefaultWindow()
            {
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Topmost = true,
                SnapsToDevicePixels = true,
                DataContext = viewModel
            };

            viewModel.Window = window;

            return window;
        }
    }
}

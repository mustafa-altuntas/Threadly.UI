using Microsoft.AspNetCore.Mvc;
using Threadly.UI.Models.ViewModels;

namespace Threadly.UI.Components.PostFlowVC
{
    public class PostFlowVC : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var models = new List<PostViewModel>();

            for (int i = 0; i < 5; i++)
            {

                models.Add(new PostViewModel
                {
                    AuthorId = $"AuthorId : {i}",
                    CommunityId = $"CommunityId : {i}",
                    Content = $"{i} Lorem ipsum dolor sit amet consectetur adipisicing elit. Sunt assumenda mollitia officia dolorum eius quasi.Chocolate sesame snaps apple pie danish cupcake sweet roll jujubes tiramisu.\r\n\r\nGummies bonbon apple pie fruitcake icing biscuit apple pie jelly-o sweet roll. Toffee sugar plum sugar plum jelly-o jujubes bonbon dessert carrot cake. Sweet pie candy jelly. Sesame snaps biscuit sugar plum. Sweet roll topping fruitcake. Caramels liquorice biscuit ice cream fruitcake cotton candy tart.",
                    Title = $"Test Post  {i}"
                });
            }

 

            return View(models);
        }
    }
}

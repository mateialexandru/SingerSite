using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingerSite.Models
{
    public class UpdateCollection
    {
        private List<IUpdateContext> listContexts = null;
        private int oldestEntries = 0;

        public UpdateCollection()
        {
            listContexts = new List<IUpdateContext>();
        }

        public UpdateCollection(params IUpdateContext[] contexts)
            : this()
        {
            foreach (IUpdateContext context in contexts)
            {
                listContexts.Add(context);
            }
        }

        public void addContext(IUpdateContext context)
        {
            listContexts.Add(context);
        }

        public List<IUpdateModel> getLatestModels()
        {
            List<IUpdateModel> listModels = new List<IUpdateModel>();
            foreach (IUpdateContext context in listContexts)
            {
                if (context.TableEntries != null)
                    listModels.AddRange(context.TableEntries);
            }
            IEnumerable<IUpdateModel> orderedModels = listModels.OrderBy(e => e.PostDate);
            listModels = orderedModels.ToList<IUpdateModel>();

            if (oldestEntries > 0)
            {
                listModels.RemoveRange(oldestEntries, listModels.Count);
            }

            return listModels;
        }
    }
}
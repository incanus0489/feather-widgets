﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Blogs.Model;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Blogs;

namespace Telerik.Sitefinity.Frontend.Blogs.Mvc.Models
{
    /// <summary>
    /// Provides API for working with <see cref="Telerik.Sitefinity.Blogs.Model.BlogPost"/> items.
    /// </summary>
    public class BlogPostModel : ContentModelBase, IBlogPostModel
    {
        /// <inheritdoc />
        public ParentFilterMode ParentFilterMode { get; set; }

        /// <summary>
        /// Gets or sets the type of content that is loaded.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public override Type ContentType
        {
            get
            {
                return typeof(BlogPost);
            }

            set
            {
            }
        }

        /// <inheritdoc />
        public virtual ContentListViewModel CreateListViewModelByParent(Blog parentItem, int page)
        {
            if (page < 1)
                throw new ArgumentException("'page' argument has to be at least 1.", "page");

            var query = ((BlogsManager)this.GetManager()).GetBlogPosts().Where(bp => bp.Parent.Id == parentItem.Id);
            if (query == null)
                return this.CreateListViewModelInstance();

            var viewModel = this.CreateListViewModelInstance();
            this.PopulateListViewModel(page, query, viewModel);

            return viewModel;
        }

        /// <summary>
        /// Gets the items query.
        /// </summary>
        /// <returns>The query.</returns>
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            return ((BlogsManager)this.GetManager()).GetBlogPosts();
        }
    }
}

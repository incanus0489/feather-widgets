﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.News
{
    /// <summary>
    /// SelectCustomDateInNewsWidgetOnPageBasedOnBootstrapTemplate test class.
    /// </summary>
    [TestClass]
    public class SelectCustomDateInNewsWidgetOnPageBasedOnBootstrapTemplate_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SelectCustomDateInNewsWidgetOnPageBasedOnBootstrapTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.News), 
        TestCategory(FeatherTestCategories.Selectors)]
        public void SelectCustomDateInNewsWidgetOnPageBasedOnBootstrapTemplate()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectWhichItemsToDisplay(WhichNewsToDisplay);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectCheckBox(DateName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButtonByDate();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectDisplayItemsPublishedIn(DisplayItemsPublishedIn);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SetFromDateByTyping(DayAgo);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddHour("10", true);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddMinute("2", true);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SetToDateByDatePicker(DayForward);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddHour("13", false);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddMinute("4", false);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, this.newsTitles[0]);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, this.newsTitles[1]);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(this.newsTitles));    
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageName = "NewsPage";
        private const string WidgetName = "News";
        private const string DisplayItemsPublishedIn = "Custom range...";
        private const string WhichNewsToDisplay = "Narrow selection by...";
        private const string DateName = "dateInput";
        private const int DayAgo = -1;
        private const int DayForward = 1;
        private readonly string[] newsTitles = { "Angel", "Cat" };
    }
}
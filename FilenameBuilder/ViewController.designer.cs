// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace FilenameBuilder
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSButton AddLogo { get; set; }

		[Outlet]
		AppKit.NSTextField campNameTxtBox { get; set; }

		[Outlet]
		AppKit.NSTextField colourTxtBox { get; set; }

		[Outlet]
		AppKit.NSTextField commentTxtBox { get; set; }

		[Outlet]
		AppKit.NSStepper CounterStep { get; set; }

		[Outlet]
		AppKit.NSTextField counterTxtBox { get; set; }

		[Outlet]
		AppKit.NSButton CountRejectBtn { get; set; }

		[Outlet]
		AppKit.NSTextField custCodeTxtBox { get; set; }

		[Outlet]
		AppKit.NSTextField errorOutputBox { get; set; }

		[Outlet]
		AppKit.NSTextField finishingTxtBox { get; set; }

		[Outlet]
		AppKit.NSButton fireworksButton { get; set; }

		[Outlet]
		AppKit.NSTextField heightTxtBox { get; set; }

		[Outlet]
		AppKit.NSTextField jobNumTxtBox { get; set; }

		[Outlet]
		AppKit.NSTextField notificationLabel { get; set; }

		[Outlet]
		AppKit.NSTextField origFNTxtBox { get; set; }

		[Outlet]
		AppKit.NSTextField quantTxtBox { get; set; }

		[Outlet]
		AppKit.NSButton rejectCheck { get; set; }

		[Outlet]
		AppKit.NSTextField resultTxtBox { get; set; }

		[Outlet]
		AppKit.NSTextField revVerTxtBox { get; set; }

		[Outlet]
		AppKit.NSScrollView ScrollView { get; set; }

		[Outlet]
		AppKit.NSTextField showFilePathTxtBox { get; set; }

		[Outlet]
		AppKit.NSTextField stockTxtBox { get; set; }

		[Outlet]
		AppKit.NSStepper totalPDFsStep { get; set; }

		[Outlet]
		AppKit.NSTextField totalPDFsTxtBox { get; set; }

		[Outlet]
		AppKit.NSTextField widthTxtBox { get; set; }

		[Action ("AddLogoChecked:")]
		partial void AddLogoChecked (Foundation.NSObject sender);

		[Action ("BuildBtn:")]
		partial void BuildBtn (Foundation.NSObject sender);

		[Action ("ClearButton:")]
		partial void ClearButton (Foundation.NSObject sender);

		[Action ("CounterStepValueChanged:")]
		partial void CounterStepValueChanged (Foundation.NSObject sender);

		[Action ("GetFilePath:")]
		partial void GetFilePath (Foundation.NSObject sender);

		[Action ("GetPreviousFilenames:")]
		partial void GetPreviousFilenames (Foundation.NSObject sender);

		[Action ("ImportXML:")]
		partial void ImportXML (Foundation.NSObject sender);

		[Action ("RejectCheckbox:")]
		partial void RejectCheckbox (Foundation.NSObject sender);

		[Action ("RejectCount:")]
		partial void RejectCount (Foundation.NSObject sender);

		[Action ("RemoveInvalidChar:")]
		partial void RemoveInvalidChar (Foundation.NSObject sender);

		[Action ("TotalPagesValueChanged:")]
		partial void TotalPagesValueChanged (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (campNameTxtBox != null) {
				campNameTxtBox.Dispose ();
				campNameTxtBox = null;
			}

			if (colourTxtBox != null) {
				colourTxtBox.Dispose ();
				colourTxtBox = null;
			}

			if (commentTxtBox != null) {
				commentTxtBox.Dispose ();
				commentTxtBox = null;
			}

			if (CounterStep != null) {
				CounterStep.Dispose ();
				CounterStep = null;
			}

			if (counterTxtBox != null) {
				counterTxtBox.Dispose ();
				counterTxtBox = null;
			}

			if (CountRejectBtn != null) {
				CountRejectBtn.Dispose ();
				CountRejectBtn = null;
			}

			if (custCodeTxtBox != null) {
				custCodeTxtBox.Dispose ();
				custCodeTxtBox = null;
			}

			if (errorOutputBox != null) {
				errorOutputBox.Dispose ();
				errorOutputBox = null;
			}

			if (finishingTxtBox != null) {
				finishingTxtBox.Dispose ();
				finishingTxtBox = null;
			}

			if (fireworksButton != null) {
				fireworksButton.Dispose ();
				fireworksButton = null;
			}

			if (heightTxtBox != null) {
				heightTxtBox.Dispose ();
				heightTxtBox = null;
			}

			if (jobNumTxtBox != null) {
				jobNumTxtBox.Dispose ();
				jobNumTxtBox = null;
			}

			if (notificationLabel != null) {
				notificationLabel.Dispose ();
				notificationLabel = null;
			}

			if (origFNTxtBox != null) {
				origFNTxtBox.Dispose ();
				origFNTxtBox = null;
			}

			if (quantTxtBox != null) {
				quantTxtBox.Dispose ();
				quantTxtBox = null;
			}

			if (rejectCheck != null) {
				rejectCheck.Dispose ();
				rejectCheck = null;
			}

			if (resultTxtBox != null) {
				resultTxtBox.Dispose ();
				resultTxtBox = null;
			}

			if (revVerTxtBox != null) {
				revVerTxtBox.Dispose ();
				revVerTxtBox = null;
			}

			if (ScrollView != null) {
				ScrollView.Dispose ();
				ScrollView = null;
			}

			if (showFilePathTxtBox != null) {
				showFilePathTxtBox.Dispose ();
				showFilePathTxtBox = null;
			}

			if (stockTxtBox != null) {
				stockTxtBox.Dispose ();
				stockTxtBox = null;
			}

			if (totalPDFsStep != null) {
				totalPDFsStep.Dispose ();
				totalPDFsStep = null;
			}

			if (totalPDFsTxtBox != null) {
				totalPDFsTxtBox.Dispose ();
				totalPDFsTxtBox = null;
			}

			if (widthTxtBox != null) {
				widthTxtBox.Dispose ();
				widthTxtBox = null;
			}

			if (AddLogo != null) {
				AddLogo.Dispose ();
				AddLogo = null;
			}
		}
	}
}

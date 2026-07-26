using System.Numerics;
using Content.Client.Stylesheets.Colorspace;
using Content.Client.Stylesheets.SheetletConfigs;
using Content.Client.Stylesheets.Stylesheets;
using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using static Content.Client.Stylesheets.StylesheetHelpers;

namespace Content.Client.Stylesheets.Sheetlets;

[CommonSheetlet]
public sealed class SliderSheetlet<T> : Sheetlet<T> where T: PalettedStylesheet, ISliderConfig
{
    public override StyleRule[] GetRules(T sheet, object config)
    {
        ISliderConfig sliderCfg = sheet;

        var sliderFillTex = sheet.GetTextureOr(sliderCfg.SliderFillPath, NanotrasenStylesheet.TextureRoot);

        var sliderFillBox = new StyleBoxTexture
        {
            Texture = sliderFillTex,
            Modulate = sheet.PositivePalette.TextDark,
        };

        var sliderBackBox = new StyleBoxTexture
        {
            Texture = sliderFillTex,
            Modulate = sheet.SecondaryPalette.BackgroundDark,
        };

        var sliderForeBox = new StyleBoxTexture
        {
            Texture = sheet.GetTextureOr(sliderCfg.SliderOutlinePath, NanotrasenStylesheet.TextureRoot),
            Modulate = Color.FromHex("#494949") // TODO: Unhardcode.
        };

        var sliderGrabBox = new StyleBoxSDFBox()
        {
            CornerRadius = new Vector4(sheet.PanelPalette.PanelCornerRadius),
            BackgroundColor = sheet.PanelPalette.PanelPrimary.NudgeLightness(0.2f).WithAlpha(0.9f),
            ContentMarginLeftOverride = 4f,
            ContentMarginRightOverride = 4f,
            ContentMarginTopOverride = 12f,
            ContentMarginBottomOverride = 12f,
        };

        sliderFillBox.SetPatchMargin(StyleBox.Margin.All, 12);
        sliderBackBox.SetPatchMargin(StyleBox.Margin.All, 12);
        sliderForeBox.SetPatchMargin(StyleBox.Margin.All, 12);


        // var sliderFillGreen = new StyleBoxTexture(sliderFillBox) { Modulate = Color.LimeGreen };
        // var sliderFillRed = new StyleBoxTexture(sliderFillBox) { Modulate = Color.Red };
        // var sliderFillBlue = new StyleBoxTexture(sliderFillBox) { Modulate = Color.Blue };
        // var sliderFillWhite = new StyleBoxTexture(sliderFillBox) { Modulate = Color.White };

        return new StyleRule[]
        {
            E<Slider>()
                .Prop(Slider.StylePropertyBackground, sliderBackBox)
                .Prop(Slider.StylePropertyForeground, sliderForeBox)
                .Prop(Slider.StylePropertyGrabber, sliderGrabBox)
                .Prop(Slider.StylePropertyFill, sliderFillBox),
            // these styles seem to be unused now
            // E<ColorableSlider>()
            //     .Prop(ColorableSlider.StylePropertyFillWhite, sliderFillWhite)
            //     .Prop(ColorableSlider.StylePropertyBackgroundWhite, sliderFillWhite),
            //
            // E<Slider>().Class(StyleClass.StyleClassSliderRed)
            //     .Prop(Slider.StylePropertyFill, sliderFillRed),
            // E<Slider>().Class(StyleClass.StyleClassSliderBlue)
            //     .Prop(Slider.StylePropertyFill, sliderFillBlue),
            // E<Slider>().Class(StyleClass.StyleClassSliderGreen)
            //     .Prop(Slider.StylePropertyFill, sliderFillGreen),
            // E<Slider>().Class(StyleClass.StyleClassSliderWhite)
            //     .Prop(Slider.StylePropertyFill, sliderFillWhite),
        };
    }
}

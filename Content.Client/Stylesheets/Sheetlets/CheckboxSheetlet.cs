using System.Numerics;
using Content.Client.Stylesheets.SheetletConfigs;
using Content.Client.Stylesheets.Stylesheets;
using Content.Client.UserInterface;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Utility;
using static Content.Client.Stylesheets.StylesheetHelpers;

namespace Content.Client.Stylesheets.Sheetlets;

[CommonSheetlet]
public sealed class CheckboxSheetlet<T> : Sheetlet<T> where T : PalettedStylesheet, ICheckboxConfig
{
    public override StyleRule[] GetRules(T sheet, object config)
    {
        ICheckboxConfig checkboxCfg = sheet;

        ResPath monochromeCheckboxUncheckedPath = new("Monotone/monotone_checkbox_unchecked.svg.96dpi.png");
        ResPath monochromeCheckboxCheckedPath = new("Monotone/monotone_checkbox_checked.svg.96dpi.png");

        var uncheckedTex = sheet.GetTextureOr(monochromeCheckboxUncheckedPath, NanotrasenStylesheet.TextureRoot);
        var checkedTex = sheet.GetTextureOr(monochromeCheckboxCheckedPath, NanotrasenStylesheet.TextureRoot);

        var uncheckedBoxBackground = new StyleBoxSDFBox()
        {
            BorderThickness = sheet.PanelPalette.PanelBorderThickness,
            BorderColor = sheet.PanelPalette.PanelBorderColor,
            BackgroundColor = sheet.PanelPalette.PanelSecondary,
            CornerRadius = new Vector4(sheet.PanelPalette.PanelCornerRadius * 2),
            ContentMarginBottomOverride = 4,
            ContentMarginLeftOverride = 4,
            ContentMarginRightOverride = 4,
            ContentMarginTopOverride = 4,
        };
        var checkedBoxBackground = new StyleBoxSDFBox()
        {
            BorderThickness = sheet.PanelPalette.PanelBorderThickness,
            BorderColor = sheet.PositivePalette.Element,
            BackgroundColor = sheet.PanelPalette.PanelSecondary,
            CornerRadius = new Vector4(sheet.PanelPalette.PanelCornerRadius * 2),
            ContentMarginBottomOverride = 4,
            ContentMarginLeftOverride = 4,
            ContentMarginRightOverride = 4,
            ContentMarginTopOverride = 4,
        };
        return
        [
            E<TextureRect>()
                .Class(CheckBox.StyleClassCheckBox)
                .Prop(TextureRect.StylePropertyTexture, uncheckedTex)
                .Prop(TextureRect.StylePropertyModulateSelf, sheet.PanelPalette.PanelBorderColor),
            E<TextureRect>()
                .Class(CheckBox.StyleClassCheckBox)
                .Class(CheckBox.StyleClassCheckBoxChecked)
                .Prop(TextureRect.StylePropertyTexture, checkedTex)
                .Prop(TextureRect.StylePropertyTexture, uncheckedTex)
                .Prop(TextureRect.StylePropertyModulateSelf, sheet.PanelPalette.PanelBorderColor),
            E<TguiCheckBox>()
                .Class("TguiCheckbox")
                .Prop(BoxContainer.StylePropertySeparation, 10)
                .Prop(ContainerButton.StylePropertyStyleBox, uncheckedBoxBackground)
                .Prop(TguiCheckBox.StyleClassUncheckedColor, sheet.PanelPalette.PanelBorderColor)
                .Prop(TguiCheckBox.StyleClassCheckedColor, sheet.PositivePalette.Element),
            E<TguiCheckBox>()
                .Class("TguiCheckbox")
                .Class("checked")
                .Prop(ContainerButton.StylePropertyStyleBox, checkedBoxBackground)
                .Prop(TguiCheckBox.StyleClassUncheckedColor, sheet.PanelPalette.PanelBorderColor)
                .Prop(TguiCheckBox.StyleClassCheckedColor, sheet.PositivePalette.Element),
            E<BoxContainer>().Class("TguiCheckbox").Prop(BoxContainer.StylePropertySeparation, 10),
        ];
    }
}

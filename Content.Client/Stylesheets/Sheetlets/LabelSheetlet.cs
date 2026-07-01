using Content.Client.Resources;
using Content.Client.Stylesheets.Fonts;
using Content.Client.Stylesheets.Palette;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using static Content.Client.Stylesheets.StylesheetHelpers;

namespace Content.Client.Stylesheets.Sheetlets;

[CommonSheetlet]
public sealed class LabelSheetlet : Sheetlet<PalettedStylesheet>
{
    public override StyleRule[] GetRules(PalettedStylesheet sheet, object config)
    {
        var robotoMonoBold11 = ResCache.GetFont("/Fonts/RobotoMono/RobotoMono-Bold.ttf", size: 11);
        var robotoMonoBold12 = ResCache.GetFont("/Fonts/RobotoMono/RobotoMono-Bold.ttf", size: 12);
        var robotoMonoBold14 = ResCache.GetFont("/Fonts/RobotoMono/RobotoMono-Bold.ttf", size: 14);

        var pixellari32 = ResCache.GetFont("/Fonts/Pixellari.ttf", size: 32);

        var jetbrainsMonoBold11 = ResCache.GetFont("/Fonts/JetbrainsMono/JetbrainsMono-Bold.ttf", size: 11);
        var jetbrainsMonoBold12 = ResCache.GetFont("/Fonts/JetbrainsMono/JetbrainsMono-Bold.ttf", size: 12);
        var jetbrainsMonoBold13 = ResCache.GetFont("/Fonts/JetbrainsMono/JetbrainsMono-Bold.ttf", size: 14);
        var jetbrainsMonoBold16 = ResCache.GetFont("/Fonts/JetbrainsMono/JetbrainsMono-Bold.ttf", size: 16);

        return
        [
            E<Label>()
                .Class(StyleClass.LabelHeading)
                .Font(sheet.BaseFont.GetFont(12, FontKind.Bold))
                .FontColor(sheet.HighlightPalette.Text),
            E<Label>()
                .Class(StyleClass.LabelHeadingBigger)
                .Font(sheet.BaseFont.GetFont(13, FontKind.Bold))
                .FontColor(sheet.HighlightPalette.Text),
            E<Label>()
                .Class(StyleClass.LabelSubText)
                .Font(sheet.BaseFont.GetFont(10))
                .FontColor(Color.DarkGray),
            E<Label>()
                .Class(StyleClass.LabelKeyText)
                .Font(sheet.BaseFont.GetFont(11, FontKind.Bold))
                .FontColor(sheet.HighlightPalette.Text),
            E<Label>()
                .Class(StyleClass.LabelWeak)
                .FontColor(Color.DarkGray), // TODO: you know the drill by now

            E<Label>()
                .Class(StyleClass.Positive)
                .FontColor(sheet.PositivePalette.Text),
            E<Label>()
                .Class(StyleClass.Negative)
                .FontColor(sheet.NegativePalette.Text),
            E<Label>()
                .Class(StyleClass.Highlight)
                .FontColor(sheet.HighlightPalette.Text),

            E<Button>().ParentOf(E<Label>()).Margin(new Thickness(8f, 2f)),
            E<OptionButton>().ParentOf(E<BoxContainer>()).ParentOf(E<Label>()).Margin(new Thickness(16f, 2f)),


            E<Label>()
                .Class(StyleClass.StatusGood)
                .FontColor(Palettes.Status.Good),
            E<Label>()
                .Class(StyleClass.StatusOkay)
                .FontColor(Palettes.Status.Okay),
            E<Label>()
                .Class(StyleClass.StatusWarning)
                .FontColor(Palettes.Status.Warning),
            E<Label>()
                .Class(StyleClass.StatusBad)
                .FontColor(Palettes.Status.Bad),
            E<Label>()
                .Class(StyleClass.StatusCritical)
                .FontColor(Palettes.Status.Critical),

            // Console text
            E<Label>()
                .Class(StyleClass.LabelMonospaceText)
                .Prop(Label.StylePropertyFont, robotoMonoBold11),
            E<Label>()
                .Class(StyleClass.LabelMonospaceSubHeading)
                .Prop(Label.StylePropertyFont, robotoMonoBold12),
            E<Label>()
                .Class(StyleClass.LabelMonospaceHeading)
                .Prop(Label.StylePropertyFont, robotoMonoBold14),

            E<Label>().Class("SectionHeader").Prop(Label.StylePropertyFont, jetbrainsMonoBold12),

            E<Label>().Class("InspectionUILabel").Prop(Label.StylePropertyFont, pixellari32),
        ];
    }
}

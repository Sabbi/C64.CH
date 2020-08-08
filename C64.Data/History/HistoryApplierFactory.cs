using System;

namespace C64.Data.History
{
    public static class HistoryApplierFactory
    {
        public static IHistoryApplier Get(ProductionEditProperty editProperty)
        {
            switch (editProperty)
            {
                case ProductionEditProperty.Party:
                    return new PartyApplier();

                case ProductionEditProperty.Groups:
                    return new GroupsApplier();

                case ProductionEditProperty.ReleaseDate:
                    return new PartialDateApplier();

                case ProductionEditProperty.HiddenParts:
                    return new HiddenPartsApplier();

                case ProductionEditProperty.ProductionVideos:
                    return new VideoApplier();

                case ProductionEditProperty.ProductionPictures:
                    return new ProductionPicturesApplier();

                default:
                    return new GenericHistoryApplier();
            }
        }

        public static IHistoryApplier Get(string editProperty)
        {
            var editPropertyEnum = (ProductionEditProperty)Enum.Parse(typeof(ProductionEditProperty), editProperty);
            return Get(editPropertyEnum);
        }
    }
}
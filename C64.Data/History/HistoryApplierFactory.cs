using System;

namespace C64.Data.History
{
    public static class HistoryApplierFactory
    {
        public static IHistoryApplier Get(HistoryEditProperty editProperty)
        {
            switch (editProperty)
            {
                case HistoryEditProperty.Party:
                    return new PartyApplier();

                case HistoryEditProperty.Groups:
                    return new GroupsApplier();

                case HistoryEditProperty.ReleaseDate:
                case HistoryEditProperty.FoundedDate:
                case HistoryEditProperty.ClosedDate:
                    return new PartialDateApplier();

                case HistoryEditProperty.JoinedDate:
                case HistoryEditProperty.LeftDate:
                    return new MembersPartialDateApplier();

                case HistoryEditProperty.HiddenParts:
                    return new HiddenPartsApplier();

                case HistoryEditProperty.ProductionVideos:
                    return new VideoApplier();

                case HistoryEditProperty.ProductionPictures:
                    return new ProductionPicturesApplier();

                case HistoryEditProperty.ProductionFiles:
                    return new ProductionFilesApplier();

                case HistoryEditProperty.AddProduction:
                    return new AddProductionApplier();

                case HistoryEditProperty.ProductionCredits:
                    return new ProductionCreditsApplier();

                case HistoryEditProperty.AddGroup:
                    return new AddGroupApplier();

                case HistoryEditProperty.AddGroupMember:
                    return new AddGroupMemberApplier();

                case HistoryEditProperty.MemberJobs:
                    return new MemberJobsApplier();

                case HistoryEditProperty.DeleteGroupMember:
                    return new DeleteGroupMemberApplier();

                case HistoryEditProperty.AddParty:
                    return new AddPartyApplier();

                case HistoryEditProperty.AddScener:
                    return new AddScenerApplier();

                default:
                    return new GenericHistoryApplier();
            }
        }

        public static IHistoryApplier Get(string editProperty)
        {
            var editPropertyEnum = (HistoryEditProperty)Enum.Parse(typeof(HistoryEditProperty), editProperty);
            return Get(editPropertyEnum);
        }
    }
}
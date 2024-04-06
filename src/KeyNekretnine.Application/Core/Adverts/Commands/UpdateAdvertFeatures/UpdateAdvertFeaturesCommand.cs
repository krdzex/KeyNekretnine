using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertFeatures;
public sealed record UpdateAdvertFeaturesCommand(string ReferenceId, UpdateAdvertFeaturesRequest FeaturesUpdateData) : ICommand;

# Start with the Microsoft dotnet SDK image
FROM microsoft/dotnet:sdk AS build-env

# In the build environment, set our work directory as app 
# also add packages to pull in the path from our build agent
# for packages that need to be installed.
WORKDIR /theatreers.company
ARG packages

# Only copy the csproj for now. Then the relevant packages
# from the agent, so they can be used as part of the restore
# process (this is done as part of publish task).
COPY theatreers.company/theatreers.company.csproj ./
COPY /$packages .nugets

# Copy the remaining application files. Publish the dll,
# being explicit about the release path. Starting with 
# .NET Core 2.0, you don't have to run dotnet restore 
# because it's run implicitly by all commands that 
# require a restore to occur. Hence pointing to the source
# folder inside the dotnet publish task.

COPY theatreers.company/ ./
RUN dotnet publish /theatreers.company/theatreers.company.csproj --source ".nugets" -c Release -o /theatreers.company/out

# Set the work directory as the tests directory
# Copy the tests from agent to docker environment
# Restore the packages for the original package

FROM build-env AS test-env
WORKDIR /theatreers.company.tests
COPY theatreers.company.tests/. ./
RUN dotnet restore --source "/theatreers.company/.nugets"

# Run the test output it to an XML file in the results directory
# Copy that file back to the main agent, ready for the publish
# task later on
ENTRYPOINT dotnet test --"logger:trx;LogFileName=/theatreers.company.tests/results/TEST-output.xml"  --no-restore
# Chown all the files to the app user.
RUN chmod -R o+wrx /theatreers.company.tests

# Now, let's switch to building the runtime image.
# We will use the aspnetcore-runtime image for this.
FROM microsoft/dotnet:aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build-env /theatreers.company/out .
ENTRYPOINT ["dotnet", "theatreers.company.dll"]
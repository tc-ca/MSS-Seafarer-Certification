# Introduction 

This document is to outline how the GitOps workflow for Transport Canada's Seafarers' Online Suite. It details the implementation of Azure DevOps, GitHub, Continuous Integration, Continuous Delivery, and Containerization.

# Getting Started

This document was created in a FOSS diagramming tool from [diagrams.net](https://app.diagrams.net/ "diagrams.net") (formerly known as draw.io)

This document is under version control in GitHub.

# The GitOps Workflow Diagram

<img src="./GitOps.svg">

# Textual version of the GitOps Diagram

## Product Owner
A **product backlog item (PBI)** is a new feature, change to existing an feature, bug fix, or other activity that a team may deliver in order to achieve a specific outcome is also intended to move the product forward.
**Backlog Refinement** is when the team review items on the backlog to ensure the backlog contains the appropriate items, that they are prioritized, also clarify what the PBI is asking the team to do and that the items at the top of the backlog are ready for delivery.

**Sprint planning** is where the team determines the product backlog items they will work on during that sprint and discusses their initial plan for completing those product backlog items.

**People involed:**
 - Product owner
 - Development team
 - Scrum master

## Dev Team GitOps

**Azure board** is where all of the backlog items are and it's where we find/create tasks within the PBI, which we will work on.

**GitHub** is our source control so it's where all the code lies. We have 2 branches, **master** and **develop**. Master is production ready code and develop is test code, that is waiting to be tested by the product owner.
When we start work on a PBI that requires development work we create a branch:
- If the PBI is a bug we branch from master.
- If the PBI is not a bug we branch from develop.

We **modify the code** primarily using Visual Studio 2019 Enterprise or any text editor that we are comfortable with.

When we have tested the code or are happy with it we **commit the changes**. Within the commit, we add a commit message describing what was done and also adding the task number, so it can be linked and tracked in the task. After that, the changes are pushed to GitHub.

We initiate **pull request** in order to get the code reviewed before it can get merged in the develop branch. The pull request's branch must be up to date, build, and pass all tests needed.

**Reviewing a pull request** means, making sure the code builds and runs, matches the wire-frame, and meets acceptance criteria. The reviewer can requests changes or comment where needed. If all is well, they then approve.
Whoever approves the pull request will also **merge** it.

### Development Enviornment CI/CD (Continuous integration / Continuous delivery)
Any merging of code into the develop branch will trigger our CI/CD pipeline. Our pipelines exists on AzureDevOps 
- CDNApplicationPrototype-develop-BuildTestPublishToZip-CI
- CDNApplicationPrototype-develop-BuildTestPushImageToACR-CI
- *As a temporary measure due to MTOAKenga being offline we deploy manually (The pipelines still run they just don't automaitcally update the prototype). 

Link to the prototype (Need access to VPN): [CDNApplicationPrototype-OnPrem](http://cdnapplicationprototype.azurewebsites.net/)
Link to the prototype (Temporarily unavailable): [CDNApplicationPrototype-Cloud](http://wwwappsmssdev/saf-sec-sur/4/seafarers/)
 

### Default tasks
- PO to review and close
- Review Pull Request
- Verify published build meets acceptance criteria.


# Contribute
 - Fork this project, make your suggestions via editing the diagram and initiate a Pull Request for Transport Canada to see your suggestions.
 - Make the SVG editable via double-click as shown here: https://github.com/jgraph/drawio-github
 - Link GitHook text to Project's custom `prepare-commit-msg` README.md.
 - Update this README.md file with an explanation of the steps.



# Mandatory exercise: Hand in number 3 – Developing in a workflow

This mandatory exercise reviews your skills in planning and implementing new features for a Microwave Oven System, which has already been developed and tested. The design and explanation of the Microwave Oven System can be seen in the handouts for Lecture 09.1.

**NOTE**: This exercise is *mandatory*. This means that it is a prerequisite for your admission to the exam of this course that you have handed this exercise in and have a grade of PASSED (see details below).  

You shall hand in in groups – no solo work. The guidelines for group work are stated in the note “*Teams, lab exercises and hand-ins in SW4SWT.pdf*” available on the course Brightspace site.

## 1 Hand-in Requirements

You shall hand in your solution to the exercise introduced in Lecture 10.1 and 10.2: adding new features using a structured work-flow, using git, GitLab and other tools.

Your hand-in must be a zipped release of your project, uploaded to Feedback Fruits on Brightspace.

Your hand-in must consist of:

./README.md:

1. Group number
2. Group members and study numbers
3. URL to your GitLab repo
4. Link til docs/README.md

./docs/README.md:

1. A new class diagram describing the final testable design after you have added all features
2. Relevant new sequence diagrams describing each new feature
3. A new STM diagram for the MicrowaveController class, describing the final result after you have added all features
4. Any new STM diagrams, if you have chosen to use an STM in other classes to implement the new features
5. A short description of **what** has been changed and **what** has been added in relation to the diagrams (you **don’t** have to explain **why**)
6. A short description of your decisions for the individual features
7. If you have made personal features, a full specification of those
8. **As a further part of this handin, the results from the exercise on Integration Test: a dependency tree and an integration plan must also be included in this document, with a paragraph explaining your strategy and decisions for the integration plan.**

./:

1. The original code, with all changes you found necessary as a result of adding the new features. This will be changes you have made to the original implementation classes, **and** necessary changes to the original unit tests, **and** new unit tests to test the new features, **and** changes to the main program to make a demonstration of the new features
2. A GitLab pipeline file ".gitlab-ci.yml" setup to run unit tests and coverage report. This file should be able to run on each branch (already included if you fork this repo)

On your repo the following must be available:

1. The main branch with the finished result after adding all features
2. All feature branches used for working on the new features
3. All history and other information as a result of following the workflow using GitLab for commits, merge requests (or pull requests), pushes, merges, etc. In other words, **do not clean up, do not delete the feature branches**, we will use it to grade your hand in
4. The history of all runs of pipelines on the branches

## 2  Evaluation of your hand-in

The hand-in is assessed through peer-evaluation. 24% of the score is based on your ability to peer-review other groups and 76% of the score is based on your hand-in.

The hand-ins are peer-reviewed according to eight weighted topics, which are each scored accoring to three grade levels:

* Not ok (0 point)
* Partially ok (1 point)
* Ok (2 point)

You overall score will be transfered to Brightspace and the teacher will review hand-ins and set a threshold for passing/resubmission/not passing the hand-in.

Note that your hand-in must be graded PASSED (in either first or second attempt) to be admitted to the exam.

The review topics are as follows:

### 2.1 Is the content of the hand-in ok? (10%)

The zip delivered is a release with a version number that contains:

1) README.md (Team number, a table with student numbers and names, link to groups GitLab repo)
2) Markdown documentation (doc/)
3) Solution with classlib, test and console application.
4) Appropriate .gitignore and .gitlab-ci.yaml files
5) No superflorous files (binaries, compilation ouput etc).

### 2.2 Does the documentation describe the updated software design? (10%)

1) Is an updated Class Diagram provided and is it correct?
2) Are sequence diagrams provided for each of the three new features provided?
3) Are the relevant State Machine diagrams provided and reasonably correct?

### 2.3 Does the documentation describe the integration testing strategy? (10%)

1) Is the dependency tree correct?
2) Is an integration plan provided, and is the chosen strategy argued for?

### 2.4 Has the software design been implemented? (15%)

1) Have the thee features been implemented?

### 2.5 Have well-formed tests been implemented? (15%)

1) Are new and adapted tests well chosen (corner cases, exceptions etc.) and formed?
2) Are tests mainly non-fragile, black-box?
3) Have metods, such as BVA, EP and/or ZOMBIE been used to pick relevant test cases?
4) Does the tests fully cover the code (coverage=100%)? If not, is it argued why certain sections have been exempted from unit testing?  

### 2.6 Has a proper Git branching strategy been used? (15%)

Looking at the group's Gitlab (Code/Repository Graph):

1) Has *main* been merged into each feature branch on a regular basis?
2) Has a proper merge request procedure been used to integrate features into *main*?

### 2.7 How does the group's work flow perform? (15%)

Looking at the group's Gitlab (Analyze):

1) In "Repository Analytics", do the coverage statistics indicate a good testing strategy? No testing (falling), build all & test later (big fall, big rise), build & test (even/small variations), test-driven (even)?
2) In "Contributor analytics", does it seem like the workload has been distributed between group members and that features have been worked on in parallel?
3) In "CI/CD Analytics", are pipelines mostly succesful? And are the failing ones only caused by the feature branches?

### 2.8 Do tests run and report nicely? (10%)

1) Do tests of the un-zipped release run/pass (dotnet test)?
2) Do tests seem to run on the server and represent themselves nicely when looking at the groups Gitlab (link in README.md)?

**MORE Review Topics TO FOLLOW before deadline!!!**

## 3 The Microwave Oven system

The design and explanation of the Microwave Oven System can be seen in the handouts for Lecture 09.1 and in the [MicrowaveOven Design Description](../Microwave.Docs/README.md).

The code for the system is available in this repository. One person from each group should fork the project to your own group (when forking, use group as project namespace, and set visability to internal). All members must clone it to their local computers, build and run the tests before proceeding.

In order to avoid creating merge requests to the original repository, you should remove the fork relationship to the original repository. This can be done in the settings of your GitLab project, under "General" and "Advanced". This will ensure that merge requests are only created between branches in your own repository, and not to the original repository.

**It is very important that you have your common and all local repos (on your work PCs) set up correctly before you start adding the features for this exercise. Make some simple changes from each of the team’s working PC on the default branch (main or master), push, and check that tests and coverage are run for each of you.**  

## 4 New features

This section describes some new features for the Microwave Oven. Some of these you **must** implement, some of them you are free to add if you have time.

For these new features you must work in parallel following a structured work flow, described during Lecture 10.1 and 10.2, and further specified in the next section. That is the main purpose of this mandatory exercise.

The new features **must** be unit tested. All necessary changes to the original unit tests **must also be made**. And the main program **must be changed** to demonstrate the new features when relevant.

### 4.1 Mandatory new features

The following 3 features must be added to the Microwave Oven.

1. **Add a buzzer/beeper** Support for a hardware sound buzzer must be added, and it must be used at the end of the cooking period, sending out 3 short burst of buzzing sounds. You are free to add further specifications – e.g. also using the buzzer in other situations during the operation and usage of the Microwave Oven, or making the buzzer have several sound effects. You are free to specify the interface, but there probably must be a new boundary class interfacing to the hardware, in a similar way to the other hardware, e.g. the Light. (Some help may be found here: [Console.Beep()](https://learn.microsoft.com/en-us/dotnet/api/system.console.beep?view=net-10.0))
2. **Make the power tube power configurable** in the range from 400 W to 1200 W – from the main function, when setting up and connecting all the modules **before** the system starts running. The Microwave as described and implemented has a power tube power of 700 W hard coded into it. This means that it is only usable directly for such a hardware configuration. Storing this configuration, e.g. in some attached ROM chip or file, **is not part** of this exercise. This will demand changes in several classes, e.g. their constructors and/or interfaces. The best solution is a balance between setting up the components at the system startup and making the classes more dependent on each other. So after these changes, **the configuration** of the actual power output is **only** handled in the main function, when initiating the system, but the rest of the code will not need to be changed in the future, when using the software with another power tube with a different power output.
3. **Change the time while cooking** You must add the functionality to extend the cooking time (make it longer) when the oven **is already** cooking. A simple way to do that is letting a press on the time button during cooking add an amount of seconds to the remaining time, and the cooking period will then be prolonged for that amount. The display should of course show the new remaining cooking time. You are also free to make it possible to make the time shorter, when the oven is already cooking. And perhaps add an extra button to implement the user interface for that purpose. If this function can make the time go to 0 (zero), then the oven should stop immediately.

### 4.2 Optional new features

The following features can be added to your solution.

They cannot replace any of the mandatory new features – see the previous section – unless it has been approved by the teachers.

1. **Add a second button** Add support for one more button which can be used to add **seconds** instead of whole minutes, to make it possible to specify the cooking time more precisely. Each press should add an amount of seconds to the cooking time, during the state when the cooking time is input, and it should be reflected on the display. It can do that in steps you decide, and when it wraps around to zero, it can increment the minute number or not, according to your **own** decision. Describe this properly in the journal. And not least: implement tests!

2. **Let the time steps be progressively larger** Another way to enable a more precise time setting would be to let the pressing of the time button progress from 5 s, to 10 s, to 30 s to 1 min, etc, such that short times can be input. The jumps in the cooking time for each press of the time button will be progressively larger according to a pattern you decide. Describe this properly in the journal. And not least: implement tests!

3. **Your own feature** You are also free to think of a new feature to be added. It doesn’t have to be approved by us, as long as you do the mandatory features, but it is a good idea to use us for feedback concerning the size and difficulty of adding your own idea. Describe this properly in the journal. And not least: implement tests!

## 5 Work process

The main purpose of this exercise is to practice and learn about ways to work in parallel, similar to a real world project.

You will be introduced to the general principles in the lectures 10.1 and 10.2, but for this handin you must follow the instructions described in the following sub-sections.

### 5.1 Working on a feature

To learn the most, you should distribute the features to several sub-teams, who will work in parallel on different features.

For a new feature, create a feature branch.

You must integrate **from** main often, to reduce the amount of problems, when you will integrate the new feature into main. That way will solve conflicts early.

Use commit often when working on a feature.

Make sure to push you feature branch often to GitLab.

Remember – always give your commits a meaningful description so that the history of changes as seen in the commit log will give a good explanation of what has been done.

### 5.2 Integrating into the main branch

When a feature is finished, you start the integration into main branch process.

First make a last merge from main (remember to fetch or pull).

When any problems from that are fixed, tested, committed and pushed, issue a merge-request. This merge-request **must** be reviewed by somebody else from your team in a line-by-line review.

If the merge-request is not accepted because issues are discovered, continue work on the feature branch and correct the agreed necessary changes, test, commit and push.

When a merge-request is accepted, several options for merging in the feature branch are available. Try the different kinds and observe the differences. E.g. how easy is it to follow the feature history of the project on the main branch?

Do not check "delete feature branch" as we will use it to grade your handin.

## 5 Tips and tricks

As we all – including the teachers – collect some experience with this work process and our setup, Tips and Tricks will be published on Brightspace. Keep an eye on those!

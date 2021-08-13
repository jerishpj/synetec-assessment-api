# Synetec Basic .Net API assessement

This is Synetec's basic API developer assessment.

If you are reading this, you most probably have been asked to complete this assessment as part of Synetec's interview process.

In this repository, you will find the base project and instructions on what to do with them. 

## How to complete this test

Please follow the instructions in the Instructions.pdf, found in this repository

## How to submit your completed test

To sumbit your test, please 
1. Fork this repository
2. Complete the test as per the instructions PDF 
3. Commit your changes to your (forked) repo 
4. Send us an http link to your repo that contains the completed test 

**This repo is Read-Only!!** So please don't try to open a pull request

## Changes Done:

1.  Separated business logic from Api project into a separate component SynetecAssessmentApi.Services.
2.  Implemented Dependency Injection 
3.  Added model validation & added custom validation attribute
4.  Removed unwanted usings and clean-up
5.  Removed unwanted project reference from webapi project.
6.  Added XUnit test project for testing SynetecAssessmentApi.Services component.
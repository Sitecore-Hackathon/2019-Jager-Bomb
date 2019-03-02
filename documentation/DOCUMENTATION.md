# Documentation

## Summary

**Category:** Best enhancement to the Sitecore Admin (XP) UI for Content Editors & Marketers

## What is the purpose of the module?

The Bulk Editing Module is designed to solve the problem of simultaneous/bulk editing of fields for several items.
Very often it happens, that content authors need to edit one or more the same fields for several Sitecore items of particular template (for example, change Product status).

Existing solutions like C# or Powershell scripts are helful for technology clients, but they do not fit for non technical people, which content authors and marketers in particular are.

So, we developed Bulk Editing Module that allows content editors to edit several items at once using user-friendly interface. 

## Pre-requisites

- Only Sitecore 9.1 is required
- No any other dependencies
- No any additional modules
- No services that must be enabled/configured

## Installation

Installation of the Bulk Editing Module is really simple and does not require any additional steps apart from package installation through the **Desktop/Development Tools/Installation Wizard**

1. Use the Sitecore Installation wizard to install the [package](https://github.com/Sitecore-Hackathon/2019-Jager-Bomb/blob/develop/sc.package/Items%20Bulk%20Editing%20Module-1.0.zip)
2. Done

## Configuration

As soon as Bulk Editing Module is installed, content authers will be able to see Bulk Edit command in Content Editor Context Menu.
In order to disable command, just comment configuration below:

```xml
<?xml version="1.0"?>
<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/">
  <sitecore>
    <commands>
        <command name="item:bulkedit" type="Reply.Feature.BulkEdit.Commands.BulkEditCommand, Reply.Feature.BulkEdit"/>
    </commands>
  </sitecore>
</configuration>
```

## Usage

As soon as Bulk Editing Module is installed, content authers will be able to see Bulk Edit command in Content Editor Context Menu.

The Bulk Editing Module allows users to edit several items under specific node/parent, so it means that if particular item does not have any children - Bulk Edit command will be disabled in Content Editor Context Menu.

![Bulk Edit command in Content Editor Context Menu (Disabled)](images/picture01.JPG?raw=true "Bulk Edit command in Content Editor Context Menu (Disabled)")

In order to edit a couple of items: 

1. Right-click on the parent item

![Bulk Edit command in Content Editor Context Menu (Enabled)](images/picture02.JPG?raw=true "Bulk Edit command in Content Editor Context Menu (Enabled)")

2. Click on 'Bulk Edit' option and choose template, fields of which need to be bulk-edited (assuming template of any child item to select)

![Bulk Edit Template Selection](images/picture03.JPG?raw=true "Bulk Edit Template Selection")

3. After selecting Template for Bulk Editing, user will be redirected to the Bulk Edit page. This page will contain all custom fields of selected template. 

![Bulk Edit Page](images/picture04.JPG?raw=true "Bulk Edit Page")

4. Fill all fields that need to be modified and click on 'Bulk Update' button. If some fieds should not be updated - keep them empty, and system will skip them during bulk update. 

5. Make sure, that new values are applied for all children of previously selected Template.

![Final Step](images/picture05.JPG?raw=true "Final Step](")

6. Have fun!

## To Do

1. Add recursive bulk updates (all descendants, not only children). At the moment module updates only first-level children.
2. Add support of inheritance for editable templates.
3. Beautify Bulk Edit Page/Use Speak UI instead of MVC Form.
4. Add support of other field types. At the moment only Single line/Multi line text fields are supported.
5. Add exception handling.
6. Check performance.  

## Video

Please provide a video highlighing your Hackathon module submission and provide a link to the video. Either a [direct link](https://www.youtube.com/watch?v=EpNhxW4pNKk) to the video, upload it to this documentation folder or maybe upload it to Youtube...

[![Sitecore Hackathon Video](https://img.youtube.com/vi/EpNhxW4pNKk/0.jpg)](https://www.youtube.com/watch?v=qWa17cODOjw)

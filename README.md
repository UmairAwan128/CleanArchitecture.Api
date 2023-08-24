# Process Builder System

# What is it?
A system allow for users to create any type of business process and 
define work flow, ex. Create purchase request admin will create purchase 
form and design it (add fields type textbox, dropdown list etc..) and give 
configuration for workflow firs employee fill request(purchase form) then 
goes to manger to make approval.
# Features
## 1 -Form builder:
A form is an entity with which you can collect input from the people who 
participate in your flow. Forms are predominantly used in flows like 
processes, projects, and cases. A form has three primary components -
section, field, and table.
Section: is a group of fields that are related to each other. As a Flow 
Admin, you can create a section to place a set of fields that are attributes 
of the main entity around which the flow revolves. 
For example, when you create a process workflow to streamline the 
hiring processes in your organization, a typical form in your hiring 
process will require a section named "Candidate information" to collect 
and store information about the candidates who will go through the 
hiring process. The fields that can be placed in the Candidate information 
section are First name, Last name, Work experience, Employment type, 
etc.,
Fields: Fields are the building blocks of your form. It is the primary 
medium through which you collect input from the users in a flow.
The fields like Text, Text Area, Number, Rating, Date, Date-time, Currency, 
Yes/no, User, Single-select dropdown, Multi-select dropdown, Slider, 
Checkbox . You can set field required or no.
You can add a field in two ways in a form:
- Drag and drop a field of your choice from the Fields panel to your 
right onto your form editor.
(or)
- Click any empty space in the section to see the list of fields from 
which you can search and add a relevant field easily.
## 2- Making a process workflow
Workflow is the set of steps that must be completed to finish an item. 
The workflow in a process shows the path for the data in the most ideal 
situation and also allows for alternate conditions and flows. 
Starting a workflow 
In processes, you create the workflow in the second step of the editor.
The first step says Configure who can start this process. By default, only 
the Flow Admin can start the process. If you click Change, an initiator 
settings popup will appear that lets you choose individual users or 
groups who can initiate items.
Now, you can add the steps for the workflow. You can add as many steps 
you want. To add a workflow step, click the Add button (+), and then Add 
a new step, Add a parallel branch or Add a Goto.
Adding a new step
Workflow steps are human tasks required to process the item.
- Enter a name and description for the step
- Next, choose whom to assign the task to. It can be a specific user, 
a group, or a user field in your form.
- Deadlines can be created for each step in a workflow. By default, a 
step does not have a deadline. To set up a default deadline, click 
Change on the step you want and then click + Add a deadline. 
Under Allotted time, enter the default deadline duration and then 
select either hours, minutes, or days.
- Under Email actions, you can choose to enable or disable approval 
and rejection of items in the step via email. If this is enabled, the 
assignee will receive an email with information about the item in 
the step, along with buttons to approve or reject the item. The 
Reject button will be available in the email only if the same is 
enabled inside form permissions. Approvals and rejections via 
email will be available only for items that do not require user 
inputs at the step. 
- If you click Manage field permission, you’ll be able to see what 
fields are editable, read-only, and hidden at that step. Click here to 
know more.

## Adding a parallel branch
Parallel branches serve two use cases. One is when you want two or 
more steps to happen at the same time. Each branch moves forward 
independent of what is happening in the other branches. For example, 
many of the steps in employee onboarding can happen simultaneously 
and they aren't dependent on other steps to finish first. However, the 
workflow will not proceed further until all of the branches have finished.
The second use case is to create two separate branches that are 
dependent on data in the form. If certain data is triggered, the item goes 
down one side. If not, it goes down the other.
When you add a branch, you first give the entire branch a name. Then 
you can click the Add button (+) to add steps to that branch. Steps in the 
branch can be collapsed or expanded by clicking the arrow button next 
to Change.

# Technology :
- We will use for backend (.Net core api 7, asp.net identity, 
entityframework, JWT)
- We will use for frontend (React js)

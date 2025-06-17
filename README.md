## InProgress — web-kanban board for FQW.
[![wakatime](https://wakatime.com/badge/user/018b8006-2512-45db-8277-d8e3339a3084/project/018c9fac-9a31-4742-afe6-aa0e084bd261.svg)](https://wakatime.com/badge/user/018b8006-2512-45db-8277-d8e3339a3084/project/018c9fac-9a31-4742-afe6-aa0e084bd261)
![Static Badge](https://img.shields.io/badge/build-passing-brightgreen)


A web-based application specifically developed for the CAD Department of SibSUTIS University. It was piloted in test mode for the winter and summer diploma sessions of 2024–25, assisting students and supervisors in tracking the progress of undergraduate final qualification work (Bachelor’s Thesis), providing transparency, ease of use, and effective collaboration.

---

### Table of Contents

- [Background & Motivation](#background--motivation)
- [Key Features](#key-features)
- [User Roles & Permissions](#user-roles--permissions)
- [Kanban Board Overview](#kanban-board-overview)
- [Technology Stack](#technology-stack)
- [Database Schema](#database-schema)
- [UI/UX Design](#uiux-design)
- [Using](#using)

---

## Background & Motivation

Supervising and submitting a Bachelor’s Thesis involves numerous milestones, communication steps, and documentation tasks. Traditional methods of tracking progress (email threads, spreadsheets, paper forms) often lead to delays, miscommunication, and lack of visibility for both students and advisors.

## Key Features

- **Customizable User Roles & Permissions**
  - Define granular access rights for students, supervisors, department staff, and guests.
- **Interactive Kanban Board**
  - Visualize thesis milestones as cards across customizable columns.
  - Supports drag-and-drop to update status in real time.
- **Bulletin Board**
  - Post announcements, deadlines, and general messages department-wide.
- **Notifications & Alerts**
  - Email and in-app notifications for approaching deadlines, status changes, and comments.
- **Reporting & Analytics**
  - Track overall progress statistics and identify bottlenecks.

## User Roles & Permissions

| Role              | Permissions                                                  |
| ----------------- | ------------------------------------------------------------ |
| **Student**       | Create/update own tasks, view supervisor feedback, comment.  |
| **Supervisor**    | Review and comment on student tasks, approve submissions.    |
| **Reviewer**      | Access assigned theses, provide evaluation comments.         |
| **Administrator** | Manage all users, configure roles, maintain system settings. |

## Kanban Board Overview

A Kanban board is a workflow visualization tool that helps teams optimize task flow and identify bottlenecks.

**Why Kanban?**

- Real-time visual feedback on task status.
- Continuous updates and transparency.
- Easy to configure and minimal setup.
- Intuitive drag-and-drop interface.

## Technology Stack

- ASP.NET Core 7.0 
- EF Core 7.0
- SQL Server 2019

## Database Schema
![browser_uFu18vTJVe](https://github.com/user-attachments/assets/555afc7e-2bd8-4dd9-a7a1-00750d3886dc)

## UI/UX Design

- Minimalist, intuitive interface
- Consistent brand colors (blue accent)
- Responsive layout for desktop and mobile

<img src="https://github.com/user-attachments/assets/778d8baa-3359-49d3-86a6-592a6166af60" width="500" height="150"/>
<img src="https://github.com/user-attachments/assets/165a9237-4e08-457a-b157-529a09ffc73e" width="500" height="150"/>
<img src="https://github.com/user-attachments/assets/9f140eed-d216-4f81-b5d6-0a050a9cd695" width="500" height="150"/> 
<img src="https://github.com/user-attachments/assets/457c864c-8ef5-484c-97bc-ac87b056908b" width="500" height="150"/>

## Using

Feel free to use InProgress for your moves

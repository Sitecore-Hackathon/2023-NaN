![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")
# Sitecore Hackathon 2023

![Hackathon Logo](docs/images/EDITORSCOPILOT_small.png)

## Team name
NaN

## Category
Best Enhancement to XM Cloud

## Description
**Editor's  Copilot** - is a module developed for Sitecore Cloud XM, which is a cloud-based content management system. The module is designed to provide an advanced level of support and guidance for content editors using Sitecore.

Editor's Copilot is based on OpenAI's GPT (Generative Pre-trained Transformer) model, which has been trained on a massive amount of code from open-source repositories. This makes it a powerful tool for generating high-quality.

  - The purpose of Editor's Copilot is to help editors to populate content more efficiently and accurately by reducing the time spent on writing.
  - At the first stage, it solves the problem of filling the content with test data based on the data already entered. Potentially - to tell the editor exactly what he wants to write in the future
- The model solves the problem of filling with test content by generating text and images based on the entered data (Item name, values entered in text fields). The module automatically suggests and fills in the fields of the page with text and images.



## Video link
A short video describing how it works

⟹ [Youtube](http://youtube.com)



## Pre-requisites and Dependencies

> Sitecore XM
> Sitecore CLI
> Get an API key on this service: https://platform.openai.com/account/api-keys


## Installation instructions

The easiest way to install module is a standart deploy to XM Cloud by using Sitecore CLI:

1. Run powershell in the solution root folder,
2. Connect to XM Cloud environment: `sitecore cloud environment connect -id {your_env_id} --allow-write true`,
3. Deploy solution: `sitecore cloud deployment create --environment-id {your_env_id} --upload`.

### Configuration
1. Get an API key on this service: https://platform.openai.com/account/api-keys,
2. Navigate to XM Cloud Content Editor and replace *Api Key* value for `/sitecore/system/Modules/Editors Copilot` item.

![image](https://user-images.githubusercontent.com/6066018/222931875-06ebc62a-73f8-41e9-8cab-a01a735a51f8.png)

## Usage instructions

> To enable auto AI content generation for all content items **(under /sitecore/content)** enable *Generate* checkbox in module item:
![image](https://user-images.githubusercontent.com/6066018/222932235-67cce51b-bbc6-4c2e-a5ad-84410d1e65d4.png)


> If you need AI content generation only for specific templates than:
1. Disable *Generate* checkbox in module item,
2. Inherit templates that need AI content generation from `/sitecore/templates/Feature/EditorsCopilot/Editors Copilot Template`,
3. This template has `Enable AI Content Generation` checkbox that is enabled by default, by you can disable it in future.
![image](https://user-images.githubusercontent.com/6066018/222932130-3bec9ab7-c52d-4787-82a6-6584cf11d6b9.png)


![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")



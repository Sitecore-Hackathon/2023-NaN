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
- Multilanguage support

![image](https://user-images.githubusercontent.com/6066018/222935663-262bad7b-d1f3-4deb-a90d-b416b8856abf.png)


## Video link

A short video describing how it works

[Youtube](https://youtu.be/igXMV-mTR1w)


## Pre-requisites and Dependencies

- Sitecore XM
- Sitecore CLI
- Get an API key on this service: https://platform.openai.com/account/api-keys


## Installation instructions

The easiest way to install module is a standart deploy to XM Cloud by using Sitecore CLI:

1. Run powershell in the solution root folder,
2. Connect to XM Cloud environment: `sitecore cloud environment connect -id {your_env_id} --allow-write true`,
3. Deploy solution: `sitecore cloud deployment create --environment-id {your_env_id} --upload`.

Full list of commands required:

```powershell
dotnet tool restore                                                                 # restore all tools
dotnet sitecore cloud login                                                         # login in xmcloud
dotnet sitecore cloud project list                                                  # to get list of projects
dotnet sitecore cloud environment list --project-id <project-id>                    # to get environment by project id
dotnet sitecore cloud deployment create --environment-id <environment-id> --upload  # deploy the application
```

More details can be found in the [documentation](https://doc.sitecore.com/xmc/en/developers/xm-cloud/walkthrough--creating-an-xm-cloud-project-using-the-sitecore-cli.html).


### Configuration

After installation, make the following settings:

1. Get an API key on this service: https://platform.openai.com/account/api-keys,

2. Navigate to XM Cloud Content Editor and replace *Api Key* value for `/sitecore/system/Modules/Editors Copilot` item.

![image](https://user-images.githubusercontent.com/6066018/222931875-06ebc62a-73f8-41e9-8cab-a01a735a51f8.png)

3. To enable auto AI content generation for all content items **(under /sitecore/content)** enable *Generate* checkbox in module item in `/sitecore/system/Modules/Editors Copilot`:

![image](https://user-images.githubusercontent.com/6066018/222932235-67cce51b-bbc6-4c2e-a5ad-84410d1e65d4.png)

4. If you need AI content generation only for specific templates than:
- Disable *Generate* checkbox in module item
- Inherit templates that need AI content generation from `/sitecore/templates/Feature/EditorsCopilot/Editors Copilot Template`
-  This template has `Enable AI Content Generation` checkbox that is enabled by default, by you can disable it in future.
![image](https://user-images.githubusercontent.com/6066018/222932130-3bec9ab7-c52d-4787-82a6-6584cf11d6b9.png)

5. Multi-language support
- Create the required language version for item `/sitecore/system/Modules/Editors Copilot` (e.g. Spanish [es-ES])
- Populate 'Phrases' section with translated texts:
![image](https://user-images.githubusercontent.com/17599747/222935633-243b8758-439c-48e3-9fb4-04c6a75b88c1.png)


## Usage instructions

When you create new content item content with be generated automatically based on item`s name.

Additionaly, for any item you can use context menu to populate content with AI:

1. Click `Fill with AI content`

![image](https://user-images.githubusercontent.com/6066018/222933143-0373ee3f-8654-45cb-9ff9-70bd6dc521d7.png)

2. Type short topic for content generation:

![image](https://user-images.githubusercontent.com/6066018/222933379-eba5718c-3477-41ff-9a24-169e562de76f.png)

3. To generate content in another language, rather then English, select the required language and use `Insert -> New Item` or `Fill with AI content`. The content should be generated in the item language (if the translations are provided in `/sitecore/system/Modules/Editors Copilot` - see **Configuration** section)
![image](https://user-images.githubusercontent.com/17599747/222935511-08edd79b-9b86-4806-99d2-e2b753503175.png)




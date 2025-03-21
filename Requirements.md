# Lock Note Requirements Sheet

## Overview

Lock Note is a secure note-sharing application that allows users to create one-time readable notes with optional password protection. Notes are stored temporarily and self-destruct after being read. The solution is deployed in Azure with Infrastructure as Code (IaC).

## Features

### Core Features

- **Create Notes**: Users can write a note and generate a unique link to share it. ✅
- **One-Time Readability**: Each note can only be accessed once or a specified number of times, after which it is deleted permanently. ✅
- **Password Protection (Optional)**: Users can set a password for additional security and encryption. ✅
- **Expiration Time**: Notes automatically expire after a month if not accessed. ✅

### Security

- **End-to-End Encryption**: Notes are encrypted before storage when a password is used. SSL is used for secure transport of the message. ✅

- **Self-Destruct Mechanism**: Once a note is accessed (a number of times), it is immediately deleted. ✅

- **No Data Retention**: No copies of the note are stored after deletion. ✅

## Technical Requirements

### Frontend

- **Framework**: React 18
- **UI**: Minimal and user-friendly design
- **API Communication**: Fetch notes securely from the backend

### Backend

- **Language**: C# with .NET 8
- **API**: RESTful endpoints for note creation and retrieval
- **Storage**: Azure Table Storage or CosmosDB (Ephemeral storage model)
- **Encryption**: AES or another suitable encryption algorithm

### Deployment

- **Cloud Provider**: Microsoft Azure
- **Infrastructure as Code**: Bicep for provisioning resources
- **Hosting**: Azure Function App (serverless architecture)
- **Deployment Automation**: GitHub Actions for CI/CD pipeline

## User Flow

1. **User creates a note** → The backend encrypts and stores it → A unique link is generated.
2. **User shares the link** → The recipient accesses it.
3. **Recipient opens the note** → The backend decrypts and returns the content → The note is permanently deleted.
4. **If the note is expired or already accessed** → The recipient sees an error message.

## Additional Considerations

- **Rate Limiting**: Prevent brute-force attacks on password-protected notes.
- **Logging & Monitoring**: Basic logging for debugging and monitoring usage.
- **Custom Expiry Times**: Users can optionally set custom expiration durations.

## Future Enhancements

- **Multi-use Notes**: Allow limited access instead of one-time reading.
- **Mobile-friendly UI**: Optimize for better mobile experience.
- **Browser Extension**: Quick note creation via an extension.

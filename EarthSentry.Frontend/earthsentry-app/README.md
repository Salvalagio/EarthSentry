# EarthSentry Frontend

Welcome to the EarthSentry Frontend!  
This is a React + TypeScript web application that lets users share, discuss, and vote on environmental posts.  
It features dark/light mode, infinite scrolling, authentication, and more.

---

## 🚀 Features

- User authentication
- Feed with infinite scroll
- Post details and comments
- Upvote/downvote system
- Admin reporting
- Dark/Light mode toggle
- Responsive design

---

## 🛠️ Getting Started

### Prerequisites

- [Node.js](https://nodejs.org/) (v16 or higher recommended)
- [npm](https://www.npmjs.com/) or [yarn](https://yarnpkg.com/)

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/your-username/earthsentry-frontend.git
   cd earthsentry-frontend/earthsentry-app
   ```

2. **Install dependencies:**
   ```bash
   npm install
   # or
   yarn install
   ```

3. **Start the development server:**
   ```bash
   npm start
   # or
   yarn start
   ```

4. **Open your browser:**  
   Visit [http://localhost:3000](http://localhost:3000) to view the app.

---

## ⚙️ Configuration

- The frontend expects the backend API to be running at `http://localhost:5253`.
- You can change the API URL in `src/services/PostService.ts` if needed.

---

## 🧑‍💻 Project Structure

```
earthsentry-app/
├── src/
│   ├── components/      # Reusable UI components
│   ├── contexts/        # React context providers
│   ├── interfaces/      # TypeScript interfaces and types
│   ├── pages/           # Page components (Feed, Login, PostDetail, etc.)
│   ├── services/        # API service functions
│   ├── types/           # Shared type definitions
│   └── App.tsx          # Main app component
├── public/
├── package.json
└── README.md
```

---

## 📝 Scripts

- `npm start` – Start the development server
- `npm run build` – Build for production
- `npm test` – Run tests (if available)
- `npm run lint` – Lint the code

## 📫 Contact

For questions or support, open an issue or contact the maintainer.

---

Enjoy using EarthSentry! 🌍
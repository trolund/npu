/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      animation: {
        float: "float 45s infinite ease-in-out",
      },
      keyframes: {
        float: {
          "0%": { transform: "translate(0, 0) scale(1)" },
          "10%": { transform: "translate(50px, -50px) scale(1.1)" },
          "20%": { transform: "translate(50px, -70px) scale(1.1)" },
          "40%": {
            transform: "translate(100px, -150px) scale(1.2) rotate(45deg)",
          },
          "50%": {
            transform:
              "translate(-50px, 230px) scale(2) rotate(90deg) opacity(0.3)",
          },
          "60%": { transform: "translate(150px, -200px) scale(1.3)" },
          "70%": {
            transform: "translate(230px, -250px) scale(1) rotate(145deg)",
          },
          "80%": { transform: "translate(300px, -300px) scale(1.1)" },
          "90%": {
            transform: "translate(90px, -140px) scale(1.1) rotate(45deg)",
          },
          "100%": { transform: "translate(0, 0) scale(1)" },
        },
      },
    },
  },
  plugins: [],
};

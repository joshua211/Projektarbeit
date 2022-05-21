module.exports = {
    important: true,
    purge: {
        enabled: true,
        content: [
            './**/*.html',
            './**/*.razor'
        ],
    },
    darkMode: false, // or 'media' or 'class'
    theme: {
        minWidth: {
            "screen": "100vw"
        },
        minHeight: {
            "screen": "100vh"
        },
    },
  variants: {
    extend: {},
  },
  plugins: [],
}

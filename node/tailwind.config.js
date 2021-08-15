const defaultTheme = require('tailwindcss/defaultTheme');

module.exports = {
    purge: {
        enabled: true,
        content: ['../**/input/**/*.cshtml'],
    },
    mode: 'jit',
    darkMode: false, // or 'media' or 'class'
    theme: {
        extend: {
            fontFamily: {
                sans: ['Inter', ...defaultTheme.fontFamily.sans],
            },
            typography: (theme) => ({
                DEFAULT: {
                    css: {
                        fontFamily: `${theme('fontFamily.sans')}`,
                        letterSpacing: theme('letterSpacing.wide'),
                        lineHeight: theme('lineHeight.relaxed'),
                    },
                },
            }),
        },
    },
    variants: {
        extend: {},
    },
    plugins: [require('@tailwindcss/typography')],
};

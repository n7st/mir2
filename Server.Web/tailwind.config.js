/** @type {import('tailwindcss').Config} */
const defaultTheme = require('tailwindcss/defaultTheme');

export const darkMode = 'media';

export const content = [
  './Views/**/*.cshtml',
  './node_modules/flowbite/**/*.js',
];

export const theme = {
  extend: {
    fontFamily: ['Inter var', ...defaultTheme.fontFamily.sans],
  },
};

export const plugins = [
  require('flowbite/plugin'),
];

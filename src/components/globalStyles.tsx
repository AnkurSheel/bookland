import { css, Global } from '@emotion/core';
import React from 'react';

const GlobalStyles = () => (
    <Global
        styles={css({
            '*, *:before, *:after': {
                boxSizing: 'border-box',
            },
            body: {
                color: '#555',
                margin: 0,
                padding: 0,
                fontFamily: '-apple-system, BlinkMacSystemFont, "Segoue UI", Roboto, Arial, sans-serif',
                lineHeight: 1.5,
                fontsize: 16,
                '> div': {
                    marginTop: 0,
                },
            },
        })}
    ></Global>
);

export default GlobalStyles;

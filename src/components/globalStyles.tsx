import React from 'react';
import { Global, css } from '@emotion/core';

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

                // display and flexDirection is used to not show the extra vertical scroll bar when minHeight is 100vh.
                // https://stackoverflow.com/questions/42508504/min-height-100vh-creates-vertical-scrollbar-even-though-content-is-smaller-than
                minHeight: '100vh',
                display: 'flex',
                flexDirection: 'column',
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

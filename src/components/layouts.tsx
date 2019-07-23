import React, { ReactNode } from 'react';
import GlobalStyles from './globalStyles';
import { css } from '@emotion/core';
import Header from './header';

interface LayoutProps {
    children: ReactNode;
}

const Layout = ({ children }: LayoutProps) => (
    <>
        <GlobalStyles />
        <Header />
        <main
            css={css`
                margin: 2rem auto 4rem;
                max-width: 90vw;
                width: 550px;
            `}
        >
            {children}
        </main>
    </>
);

export default Layout;

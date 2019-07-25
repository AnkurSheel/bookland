import React, { ReactNode } from 'react';
import GlobalStyles from './globalStyles';
import { css } from '@emotion/core';
import Header from './header';
import Helmet from 'react-helmet';
import useSiteMetaData from '../hooks/useSiteMetadata';
import { SiteSiteMetadata } from '../graphqlTypes';

interface LayoutProps {
    children: ReactNode;
}

const Layout = ({ children }: LayoutProps) => {
    const { title, description } = useSiteMetaData() as SiteSiteMetadata;

    return (
        <>
            <GlobalStyles />
            <Helmet>
                <html lang="en"></html>
                <title>{title}</title>
                <meta name="description" content={description}></meta>
            </Helmet>
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
};

export default Layout;

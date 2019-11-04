import { css } from '@emotion/core';
import React, { ReactNode } from 'react';
import Helmet from 'react-helmet';
import { SiteSiteMetadata } from '../graphqlTypes';
import useSiteMetaData from '../hooks/useSiteMetadata';
import GlobalStyles from './globalStyles';
import Header from './header';

interface LayoutProps {
    children: ReactNode;
}

const styles = {
    header: css({
        margin: '2rem auto 4rem',
        maxWidth: '90vw',
        width: 800,
    }),
};

const Layout = ({ children }: LayoutProps) => {
    const { title, description } = useSiteMetaData() as SiteSiteMetadata;

    return (
        <>
            <GlobalStyles />
            <Helmet>
                <html lang="en"></html>
                <title>{title}</title>
                <meta name="description" content={description || ''}></meta>
            </Helmet>
            <Header />
            <main css={styles.header}>{children}</main>
        </>
    );
};

export default Layout;
